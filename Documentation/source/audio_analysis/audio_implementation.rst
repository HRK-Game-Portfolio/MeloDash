.. figure:: ../_static/index/Cover.jpg
    :align: center

Beat Detection C# Inplementation
================================

The BeatDetection class has 2 modes for detecting beats by:

* computing the energy of all frequency range, 
* sensing beat in particular frequency ranges.

We used two methods at the same time not only because it can be more accurate interms of capturing the beat, but also various game features will correspond to different types of beat.

SOUND ENERGY ``isBeatEnergy()``:
---------------------------------

If the ear intercepts a monotonous sound with big energy peaks superior to the sound’s energy history, it will detect a beat. However, if a continuous loud sound is played we cannot perceive any beat.

The instant energy will be contained in 1024 samples, which is about 5 hundredth of a second. Because some songs have both intense and calm parts, the average energy should be computed nearby the instant energy. Therefore, we detect a beat only when the energy is superior to a local energy average.

Firstly, in the stereo mode, we use 1024 new samples taken in left and right channels (frames0, frames1) to compute the instant energy level:

.. math::
    e = e_{stereo} = e_{right} + e_{left} = \sum_{k=i_0}^{i_0+1024} a[k]^2 + b[k]^2


.. code-block:: C#

    GetComponent<AudioSource>().GetOutputData(frames0, 0);
    GetComponent<AudioSource>().GetOutputData(frames1, 1);

    ...

    bool isBeatEnergy() {
        float level = 0f;
        for (int i = 0; i < sampleRange; i++) {
            // frame0, frame1 corresponding to left, right channel
            // level refers to the instant energy
            level += ((float)Math.Pow(frames0[i], 2)) + ((float)Math.Pow(frames1[i], 2));
        }

        ...

Then we compute the local average energy E on the 44100 samples(1 seconds). Assuming that the hearing system only remembers 1 second of song to detect beat, and there are 44032 samples in 1 second:

.. math::
    <E> = \frac{1024}{44100} \times \sum_{i=0}^{44032} (B[0][i])^2 + (B[1][i])^2

.. math::
    <E> = \frac{1}{43} \times \sum_{i=0}^{43} (E[i])^2

.. code-block:: C#

    float E = 0f;
    for (int i = 0; i < numHistory; i++) {
        E += energyHistory[i];
    }

    if (numHistory > 0) {
        E /= (float)numHistory;
    }

``energyHistory[circularHistory] = instant`` assigns the instant energies history to (E) so we don’t need to compute average energy on the 44100 samples buffer. (E) must corresponds to about 1 second of the music, which is the energy history of 44032 samples if the sample rate is 44100 samples per second. For instance, we will have 43 energy values in ``energyHistory``, each computed on 1024 samples which makes 44032 samples energy history, and that is equivalent to 1 second in real life. ``energyHistory[0]`` will contain the oldest energy value computed from oldest 1024 samples.

``C`` CONSTANT
~~~~~~~~~~~~~~

To make the beat detection more reliable and adaptable to various type of music, `C` constant was introduced by Frederic Pakin [1] to automatically determine the sensibility of the algorithm to the beat. It is used by comparing instant energy to C*E, if instant energy is superior to C*E, then the beat is detected! However, the value of C varies is dependent to the music itself. For example, rap music beats are usually quite intense and its C constant is around 1.4, while rock and rock contains a lot of noise and the beats are more ambiguous and ‘C’ is quite low(1 or 1.1) .
To deal with this, we calculate the variance of the energies from the ``energyHistory``:

.. math::
    V = \frac{1}{43} \times \sum_{i=0}^{43} (E[i] - <E>)^2

.. code-block:: C#

    float V = 0f;
    for (int i = 0; i < numHistory; i++) {
        V += (energyHistory[i] - E) * (energyHistory[i] - E);
    }

    if (numHistory > 0) {
        V /= (float)numHistory;
    }

The variance will tell us how clear the beats of the song are and provide us a way to compute the optimal ``C`` constant by:

.. math::
    C = (-0.0025714f \times V) + 1.5142857

**Comparison**

If the instant energy is greater than :math:`C \times E`, a beat is then found and the BeatDetection.cs will fire an energy event to the SpawnManager.cs to generate corresponding obstacle:

.. code-block:: C#

    bool detected;
    if (Time.time - tIni < MIN_BEAT_SEPARATION) {
        detected = false;
    } else if (diff2 > 0.0 && instant > 2.0) {
        detected = true;
        tIni = Time.time;
    } else {
        detected = false;
    }
