using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    //Set your screenshot resolutions
    public int captureWidth = 1920;
    public int captureHeight = 1080;
    public int rectX, RectY;
    public ScreenshotViewer screenshotViewer;

    // configure with raw, jpg, png, or ppm (simple raw format)
    //public enum Format { RAW, JPG, PNG, PPM };
    //public Format format = Format.JPG;

    // folder to write output (defaults to data path)
    private string outputFolder;
    private string specificFileName;

    // private variables needed for screenshot
    private Rect rect;
    private RenderTexture renderTexture;
    private Texture2D screenShot;
    private bool isProcessing;

    //Initialize Directory
    private void Start()
    {
        outputFolder = Application.dataPath + "/Resources/Screenshots/";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
            Debug.Log("Save Path will be : " + outputFolder);
        }

    }


    private string CreateFileName(int width, int height)
    {
        //timestamp to append to the screenshot filename
        string timestamp = DateTime.Now.ToString("yyyyMMddTHHmmss");
        // use width, height, and timestamp for unique file 
        var filename = string.Format("{0}/screen_{1}x{2}_{3}.{4}", outputFolder, width, height, timestamp, "png");
        var splitNames = filename.Split('/');
        specificFileName = splitNames[splitNames.Length - 1].Split('.')[0];
        // return filename
        return filename;
    }

    private void CaptureScreenshot()
    {
        isProcessing = true;
        // create screenshot objects
        if (renderTexture == null)
        {
            // creates off-screen render texture to be rendered into
            rect = new Rect(rectX, RectY, captureWidth, captureHeight);
            renderTexture = new RenderTexture(captureWidth, captureHeight, 24);
            screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        }
        // get main camera and render its output into the off-screen render texture created above
        Camera camera = Camera.main;
        camera.targetTexture = renderTexture;
        camera.Render();
        // mark the render texture as active and read the current pixel data into the Texture2D
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(rect, 0, 0);
        // reset the textures and remove the render texture from the Camera since were done reading the screen data
        camera.targetTexture = null;
        RenderTexture.active = null;
        // get our filename
        string filename = CreateFileName((int)rect.width, (int)rect.height);
        // get file header/data bytes for the specified image format
        byte[] fileHeader = null;
        byte[] fileData = null;
        //Set the format and encode based on it
        fileData = screenShot.EncodeToPNG();
        // create new thread to offload the saving from the main thread
        new System.Threading.Thread(() =>
        {
            var file = System.IO.File.Create(filename);
            if (fileHeader != null)
            {
                file.Write(fileHeader, 0, fileHeader.Length);
            }
            file.Write(fileData, 0, fileData.Length);
            file.Close();
            Debug.Log(string.Format("Screenshot Saved {0}, size {1}", filename, fileData.Length));
            isProcessing = false;
            screenshotViewer.OnImageReady(specificFileName);
        }).Start();
        //Cleanup
        Destroy(renderTexture);
        renderTexture = null;
        screenShot = null;
    }

    public void TakeScreenShot()
    {
        if (!isProcessing)
        {
            CaptureScreenshot();
        }
        else
        {
            Debug.Log("Currently Processing");
        }
    }


}