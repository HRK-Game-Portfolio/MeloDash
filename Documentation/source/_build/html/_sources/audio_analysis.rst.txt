Audio Analysis
==============

MaxMSP Prototype
----------------



C# Inplementation
-----------------

Beat Detection
~~~~~~~~~~~~~~


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

.. math::
    C = (-0.0025714f \times V) + 1.5142857

**Comparison**

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

