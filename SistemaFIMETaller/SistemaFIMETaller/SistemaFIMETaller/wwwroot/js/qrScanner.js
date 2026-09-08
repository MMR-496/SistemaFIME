let qrStream = null;
let scanning = false;

window.startQrCameraScan = async (videoElementId, dotNetHelper) => {

    if (scanning) return; 
    scanning = true;

    const video = document.getElementById(videoElementId);
    if (!video) {
        scanning = false;
        return;
    }

    try {
        qrStream = await navigator.mediaDevices.getUserMedia({
            video: { facingMode: "environment" }
        });

        video.srcObject = qrStream;
        video.setAttribute("playsinline", true);

        await video.play();

        const canvas = document.createElement("canvas");
        const context = canvas.getContext("2d");

        const scan = () => {
            if (!scanning) return;

            if (video.readyState === video.HAVE_ENOUGH_DATA) {
                canvas.width = video.videoWidth;
                canvas.height = video.videoHeight;

                context.drawImage(video, 0, 0, canvas.width, canvas.height);

                const imageData = context.getImageData(0, 0, canvas.width, canvas.height);
                const code = jsQR(imageData.data, imageData.width, imageData.height);

                if (code) {
                    dotNetHelper.invokeMethodAsync("OnQrScanned", code.data);
                    stopQrCamera();
                    return;
                }
            }

            requestAnimationFrame(scan);
        };

        scan();

    } catch (err) {
        console.error("Error cámara QR:", err);
        scanning = false;
    }
};

window.stopQrCamera = () => {
    scanning = false;

    if (qrStream) {
        qrStream.getTracks().forEach(t => t.stop());
        qrStream = null;
    }
};


