Music Visualisation
-------------------

A set of spectrum has been placed as background decoration indicating the beat detections:

.. figure:: ../_static/graphic_design/background/spectrum.png
    :align: center

    Beat Generated Wave Spectrum

.. code-block:: C#

    public float startScale, maxScale;

    ...

    void Update() {
        if (useBuffer) {
            transform.localScale = new Vector3(
                (AudioHelper.amplitude * maxScale) + startScale,
                (AudioHelper.amplitude * maxScale) + startScale,
                (AudioHelper.amplitude * maxScale) + startScale);
        } else {
            transform.localScale = new Vector3(
                (AudioHelper.amplitude * maxScale) + startScale,
                (AudioHelper.amplitude * maxScale) + startScale,
                (AudioHelper.amplitude * maxScale) + startScale);
        }
    }