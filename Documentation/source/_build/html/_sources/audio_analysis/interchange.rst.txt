Since more beat-related features will be added to the game, isBeatFrequency() was written to detects big energy variations in particular frequency sub-bands. 

The same method is used in the frequency mode, but instead of computing the buffer, an FFT is used to get a spectrum and is then divided into average bands. These bands are tracked to detect beats in three frequency bands, low, medium and high. BeatDetection.cs will fire an event, either Kick for low, Snare for medium and Hit Hat for high, whenever any of this beats is detected.

Instead of equally split the full spectrum or using the linearly spaced averages, we used the logarithmically spaced averages of octaves to separate the spectrum. One frequency is an octave above another when the frequency is twice of the
lower, which is much more useful in our case because the octaves map more directly to how humans perceive sound. [3]

We need to find the total number of octaves which is calculated by dividing the Nyquist frequency by 2, and the result of of it by 2 [2], and so on:

.. code-block:: C#

    // number of samples per block nyquist limit
    float nyq = (float)sampleRate / 2f;
    octaves = 1;
    while ((nyq /= 2) > minFrequency) {
        octaves++;
    }

Then every octaves are splitted equally into 3 bands. The lower&upper frequency of each octave as well as each bandwidth will be used to track the amplitude of every bands throughout the spectrum:

.. figure:: ../_static/beat_detection/individual_freq_student.jpg
    :align: center

.. note:: After the beat is detected, function ``isRange()`` will check which frequency range it is in and choose the correspondent event to sent to SpawnManager.cs.
