---
# Feel free to add content and custom Front Matter to this file.
# To modify the layout, see https://jekyllrb.com/docs/themes/#overriding-theme-defaults

layout: default
title:  System Generator (Xilinx ISE)
parent: Xilinx
grand_parent: Study
nav_order: 1
---

필자는 과거에 단종된 Spartan-6를 이용해 System Generator로 Code generation 후, 이를 튜닝하여 G-CAM에 사용했던 FPGA를 만들었던 기억이 있다. 워낙 과거의 일이기도 하지만, 레거시라도 설치법을 남겨두는 것이 좋을 것 같아 기록한다.

<p align="center">
<img src="Image/Fig1.PNG"/>
</p>

Spartan-6는 ISE 구버전에서 사용할 수 있으며, 이후의 Vivado나, ISE 신형 OS 버전에서는 이를 사용할 수 없다. 아마 ISE를 버리고 Vivado로 SDK를 새로 만드는 과정에서 버릴 건 버리려는 AMD(Xilinx)의 의도가 아니였을까 싶다.

그래서 굳이 사용하려면, 가상화 도구를 통해 Windows 7을 설치하고 MATLAB과 ISE 14.7(구버전)을 설치하여야 한다.

필자는 당시 Matlab 2017b를 사용했었고 Fitting Curve Toolbox와 Simulink를 선택해서 사용하였다.


<p align="center">
<img src="Image/Fig3.PNG"/>
</p>

다음으로는 ISE를 설치하는데, Webpack Edition엔 System Generator 기능이 없으므로, System Edition을 설치하도록 한다.

<p align="center">
<img src="Image/Fig4.PNG"/>
</p>

Matlab이 만일 먼저 깔려있다면, ISE가 이를 인식해서 System Generator를 설정할 수 있도록 도와준다.

<p align="center">
<img src="Image/Fig5.PNG"/>
</p>

Matlab을 먼저 깔지 않았더라도, 이후에 Matlab의 버전을 선택해서 설정할 수 있도록 시작 페이지에서 확인 할 수 있다.

<p align="center">
<img src="Image/Fig6.PNG"/>
</p>

이후 System Generator를 통해 Matlab을 실행시키면 Simulink를 통해 System Generation으로 Code Generation이 가능하도록 만들 수 있다.


예전에 했던 내용들이라 굳이 적을 필요가 없었지만, 이걸 적었던 이유는 최근 이와 관련된 면접에서 굉장히 불쾌한 경험을 했기 때문이다. 필자는 과거에 했던 기억을 통해 이런 식으로 구현했다고 면접에서 언급했는데, 면접관은 "내가 알고 있는 것과는 다른데요?"라던지 "그건 틀렸어요." 라고 얘기했기 때문이다.

무슨 Toolbox를 사용했었는지도 제대로 얘기했고 Toolbox Licence에 대해서 제대로 얘기해줘도 자기가 알고 있는 것과는 다르다는 이유만으로 면접자에게 틀렸다고 단정지어버리는, 굉장히 불쾌한 면접관을 만났다. 그 면접관을 통해 그 기업의 그릇을 볼 수 있었다. 다행이다.