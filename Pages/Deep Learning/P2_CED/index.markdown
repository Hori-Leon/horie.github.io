---
# Feel free to add content and custom Front Matter to this file.
# To modify the layout, see https://jekyllrb.com/docs/themes/#overriding-theme-defaults

layout: default
title:  CED (Conv'al Enc-Dec)
parent: Deep leanring
has_children: false
---

# CED (Convolutional Encode-Decode)
{: .no_toc }

## Table of contents
{: .no_toc .text-delta }

1. TOC
{:toc}

---

## Semantic Segmentation

{: .v-align-middle}
<img src="Image/Fig1.PNG"/>

{: .v-align-middle}
<img src="Image/Fig2.jpeg"/>


입력 이미지에서 찾아낸 Class들을 Pixel 단위로 본류해주는 것을 말한다.

 ---

## FCL (Fully Connected Layer)


## FCN (Fully Convolutional Networks)

[FCN에 대한 논문은 다음에서 확인할 수 있다.](Resource/FCN.pdf)

{: .highlight }
Typical recognition nets, including LeNet, AlexNet, and its deeper successors, ostensibly take fixed-sized inputs and produce non-spatial outputs. The fully connected layers of these nets have fixed dimensions and throw away spatial coordinates. However, these fully connected layers can also be viewed as convolutions with kernels that cover their entire input regions. Doing so casts them into fully convolutional networks that take input of any size and output classification maps. This transformation is illustrated in Figure 2.

{: .v-align-middle}
<img src="Image/Fig3.PNG"/>

 하지만 AlexNet과 같은 검출 알고리즘은 그 성능이 검증되었음에도 불구하고 고정적인 크기의 입력값이 필요하고 연속적인 Convolution 연산에 위치 좌표(Spatial coordinates)를 잃어버린다는 한계성이 있다.


---

## SegNet

[SegNet에 대한 논문은 다음에서 확인할 수 있다.](Resource/SegNet.pdf)


## 실증 코드
