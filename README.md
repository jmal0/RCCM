# RCCM

Repository for the FAA William J. Hughes Technical Center's Remote Controlled Crack Monitoring System (RCCM) user interface. This interface is being developed for a Drexel University 2016-17 MEM Senior Design project

## Dependencies

The hardware and software for this project require the following dependencies to be installed:

* [Imaging Source Device Driver for GigE cameras](https://www.theimagingsource.com/support/downloads-for-windows/device-drivers/icwdmgigetis/)
* [IC Imaging Control .NET](https://www.theimagingsource.com/support/downloads-for-windows/software-development-kits-sdks/icimagingcontrol/)
* [Point Grey FlyCapture2 SDK](https://www.ptgrey.com/support/downloads/10700/)
* [Gardasoft Trinity SDK](http://www.gardasoft.com/Downloads/)
* [Json.NET](https://github.com/nlohmann/json)
* Microsoft .NET Framework 4 (or later)
* [TrioPC Motion](http://www.triomotion.com/tmt3/sitefiles/software/addon_software.asp#one&section=six)
 * After installing the ActiveX component, it will need to be registered. This can be done by following [this procedure](http://www.ctimls.com/Support/KB/How%20To/Register_dll.htm). Open a command prompt as administrator, enter cd C:\Windows\SysWOW64 followed by regsvr32 "C:\Program Files\TrioMotion\TrioPCMotion\TrioPC64.ocx"

The following software packages are provided by the hardware manufacturers and may be useful for degubbing issues with the hardware:

* [IC Capture](https://www.theimagingsource.com/support/downloads-for-windows/end-user-software/iccapture/) - GUI for showing WFOV live image and adjusting settings
* [Point Grey FlyCap2 Viewer](https://www.ptgrey.com/support/downloads/10655/) - GUI for showing NFOV live image and adjusting settings
* [GardasoftMaint](http://www.gardasoft.com/Downloads/) - Interface to Gardasoft TR-CL180 NFOV lens controller
* [Motion Perfect v4](http://www.triomotion.com/tmt3/sitefiles/software/motion_perfectv4.asp#section=t10) - Tool for managing Trio 8-axis motor controller.

## Installation

Before installation, install all dependencies from the list in the previous section.

### Compilation from source

Download source code from [here](https://github.com/jmal0/RCCM.git). Open RCCM.sln in Visual Studio. Info for resolving dependency references:

* TrioPCLib, AxInteropTrioPCLib: These will require you to first register the ActiveX component (see "dependencies"). Visual Studio will have to be opened as Administrator to successfully create the reference.
* TIS.Imaging.ImagingControl34: This can be located in "Documents\IC Imaging Control 3.4\redist\dotnet". Choose the x64 version of "TIS.Imaging.ICImagingControl34.dll"
* Newtonsoft.Json: After downloading the latest release .zip, add a reference to the dll in the unzipped archive.

Set the target to x64 and compile. Before running, you should ensure that the exe directory contains the following files:

* settings.json
* config/WFOV1.xml
* config/WFOV2.xml

## Usage and Functionality

* Measurement:

  Measurements of cracks can be made within the live image. First, a crack must be created by clicking the "New Sequence" button. A unique name can be specified. The color and thickness of the line to be drawn over the live image can also be modified. Also, the crack length calculation method can be speficied. "Projection" measures the crack's length in the direction specified by the orientation angle. "Tip to Tip" takes the straight line distance from crack tip to base. "Total length" sums the length of each line segment.

  Once one or more measurment sequences has been created, points can be added by left clicking within the live image. By holding right click, a segment can be drawn. To add points to a different sequence, select the crack name in the "Cracks" list box. 
  
  Measurements can also be added by using the "Measure at crosshair" button. These measurements will not be affected by an inaccurate pixel to distance calibration, so use this measurement mode when possible.

* Imaging:
 
 |Icon                                                                                   | Function         |Debugging
 |:-------------------------------------------------------------------------------------:| -----------------| ---------------------------------------------------------------------------------------------------------------------------------------------|
 | ![play.png](https://github.com/jmal0/RCCM/blob/master/RCCM/res/play.png?raw=true)     | Start live image | If image does not show or button is disabled, the camera is not connected, opened by another process, or does not have sufficient bandwidth. |
 | ![stop.png](https://github.com/jmal0/RCCM/blob/master/RCCM/res/stop.png?raw=true)     | Stop live image  |
 | ![snap.png](https://github.com/jmal0/RCCM/blob/master/RCCM/res/snap.png?raw=true)     | Snap image       | Ensure that the image output directory in the "Setup" tab is set to a writable path.
 | ![record.png](https://github.com/jmal0/RCCM/blob/master/RCCM/res/record.png?raw=true) | Record video     | Ensure that the video output directory in the "Setup" tab is set to a writable path.
 
* WFOV Camera: The WFOV camera has a user-adjustable focus and zoom. These can be controller with the sliders to the right of the live image. Also, the camera can be focused using the "Autofocus" button. Allow 2 seconds for this operation to complete.

* NFOV Camera: The NFOV camera adjusts focus automatically when the distance sensor is functioning properly. You can adjust the focus using the controls in the Settings->NFOV Lens Calibration menu. Adjust the cameras current focus using the Focal Power control. Add calibration points by clicking apply. The table of these points is displayed at the top of the window.

* Viewing measurement data: Measurement files are written to the "data" directory in the exe directory by default. Files are named with the timestamp when the first measurement was made. The file is a csv with a header followed by a row for each measurement point.

## Debugging

For issues during startup, first check that the file "settings.json" (or whatever settings file is provided as an argument) is present in the exe directory and has the same property names and types as in the settings.json included in this repository.

For other issues, please review the output file from the program execution, which is located at "log/output.txt" by default.

## Developing

Below is a summary of the purpose and function of each file and Class declared in this repository

### Core Code

* **CycleCounter.cs**
* **definitions.cs**
* **LensCalibrationForm.cs**
* **Logger.cs**
* **Measurement.cs**
* **MeasurementSequence.cs**
* **Motor.cs**: Abstract interface defining the base functionality needed for a motor implementation by the rest of the program
  * **VirtualMotor.cs**: An implementation of a Motor that requires no physical hardware. Simply stores a position value and other properties
  * **VirtualZMotor.cs**
  * **StepperMotor.cs**
  * **StepperZMotor.cs**
* **NFOV.cs**
* **NFOVLensController.cs**
* **NFOVView.cs**
* **PanelView.cs**
* **Program.cs**
* **RCCMSystem.cs**
* **Settings.cs**
* **settings.json**
* **TestResults.cs**
* **TrioController.cs**
* **WFOV.cs**

### UI

* **AboutRCCMForm.cs**
* **LensCalibrationForm.cs**
* **MotorSettingsForm.cs**
* **NewMeasurementForm.cs**
* **NFOVViewForm.cs**
* **RCCMMainForm.cs**
* **WFOVViewForm.cs**

## Known Issues

* The "Cancel" button in the WFOV properties window crashes the WFOV camera (as of 2-12-2017 on Windows 10)
* NFOV video recording does not work
* Cycle counting has not been implemented - currently a constant cycle frequency can be set which will be used to increment the cycle number periodically.

## License

The MIT license is provided with this repository. Please feel free to edit the source code as needed at your own risk.

## Contact

John Maloney
