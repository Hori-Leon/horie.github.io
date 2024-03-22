---
# Feel free to add content and custom Front Matter to this file.
# To modify the layout, see https://jekyllrb.com/docs/themes/#overriding-theme-defaults

layout: default
title:  Activate Function
parent: Machine Learning
grand_parent: Study
nav_order: 2
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

 이 문단의 설명에는 [CS231n의 Fei-Fei, Krishna, Xu의 슬라이드](Resource/CS231n.pdf)를 참고한다.

---

## 로지스틱 회귀(Logistic Regression)

### 정의
로지스틱 회귀는 결과값이 2가지만 있을 때, 이에 대한 가능성을 예측하기 위해 사용하는 통계 기법이다.
Wikipedia에서는 굉장히 어렵게 설명되어 있는데 이를 쉽게 풀어보려고 한다.


먼저, 비만과 몸무게의 상관관계에 대한 자료가 있다고 가정하자.

{: .v-align-middle}
<img src="Image/Fig1.PNG"/>

결과값이 0이면 비만이 아니며, 1이면 비만이라 가정하자.

로지스틱 함수는 이런 모양을 가지고 있다.

{: .v-align-middle}
<img src="Image/Fig2.png"/>

이를 겹쳐보자.

{: .v-align-middle}
<img src="Image/Fig6.png"/>
{: .v-align-middle}
<img src="Image/Fig7.png"/>

앞서 정의한 것처럼, 어떤 통계값에 대해 특정 결과의 값을 가능성을 함수로서 표현한 것이 되겠다.
예를 들어 x값이 90이라고 가정하였을때, 빨간 선의 y값이 그 가능성이 되는 것이다.

### 오즈(Odds)의 이해

---

## 활성 함수 종류

### 좋은 활성 함수의 조건

좋은 활성 함수의 조건은 다음과 같이 나타낼 수 있다.

* Zero-Centered : 활성 함수 중앙값이 0이여야 한다.
중앙값이 0이여

* Symmetric : 활성 함수가 대칭성이 있어야 한다.

### Rectified Linear Unit ( 비선형 연산 )

{: .v-align-middle}
<img src="Image/Fig3.jpg"/>
{: .v-align-middle}
<img src="Image/Fig4.png"/>

모든 0 이하의 입력은 전부 0으로 출력하나, 양수의 입력에선 기울기가 1인 출력을 가진다. 이 함수의 의의는 합성곱 연산 이후 가지는 합성곱의 연산 이후, 이를 오차역전파(Back-Propagation)이라고 한다.

<img src="Image/Fig5.jpg"/>

전통적으로는 이 오차 보정에 Simgoid를 사용하였으나 미분 연산의 용이성, 결정적으로 Gradient가 수행 Layer에 비례해 0으로 수렴하는 Gradient Vanishing 문제로 인해 대체되었다.

