---
# Feel free to add content and custom Front Matter to this file.
# To modify the layout, see https://jekyllrb.com/docs/themes/#overriding-theme-defaults

layout: default
title:  Activate Function
parent: Deep leanring
has_children: false
---

# Activate Function
{: .no_toc }

## Table of contents
{: .no_toc .text-delta }

1. TOC
{:toc}

---

## 개요
 활성 함수(Activation function)은 가중값이 유효한 정보인지, 유효하지 않은 정보인지 판단하는 척도로 출력하게끔 도와주는 함수다. 

 사람도 어떤 현상과 데이터에 대해 확신과 가능성을 제시하듯이, 이런 개념을 추종하는 신경망의 출력도 확신과 가능성의 출력으로 나타낼 수 있다는 생각으로 접근하면 이해하기 편하다. 예를 들어 출력값에 대한 수치가 높으면 높을 수록 Highly predictable한 값이라고 생각할 수 있을 것이다.

 이 문단의 설명에는 [CS231n의 Fei-Fei, Krishna, Xu의 슬라이드](Resource/CS231n.pdf)를 이용할 것이다.

 ## 로지스틱 회귀(Logistic Regression)

 ## Rectified Linear Unit ( 비선형 연산 )

{: .v-align-middle}
<img src="Image/Fig3.jpg"/>
{: .v-align-middle}
<img src="Image/Fig4.png"/>

모든 0 이하의 입력은 전부 0으로 출력하나, 양수의 입력에선 기울기가 1인 출력을 가진다. 이 함수의 의의는 합성곱 연산 이후 가지는 합성곱의 연산 이후, 이를 오차역전파(Back-Propagation)이라고 한다.

<img src="Image/Fig5.jpg"/>

전통적으로는 이 오차 보정에 Simgoid를 사용하였으나 미분 연산의 용이성, 결정적으로 Gradient가 수행 Layer에 비례해 0으로 수렴하는 Gradient Vanishing 문제로 인해 대체되었다.

