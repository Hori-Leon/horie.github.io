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

## 선형 회귀분석(Linear Regression)

### 정의

{: .v-align-middle}
<img src="Image/Fig9.PNG"/>

선형 회귀분석은 데이터의 결과가 선형적인 추세를 보일 때 사용하는 분석 모델로, 입력 값(x)에 대해 출력 값(y)을 예측한다.

## 로지스틱 회귀분석(Logistic Regression)

### 정의
선형 회귀분석과는 다르게 데이터의 결과값들이 선형성을 가지지 않고 참 혹은 거짓처럼 이분법적인 분포를 가질 때 사용한다. Wikipedia에서는 굉장히 어렵게 설명되어 있는데 이를 쉽게 풀어보려고 한다.

먼저, 비만과 몸무게의 상관관계에 대한 자료가 있다고 가정하자.

{: .v-align-middle}
<img src="Image/Fig1.PNG"/>

결과값이 0이면 비만이 아니며, 1이면 비만이라 가정하자.

로지스틱 함수는 이런 모양을 가지고 있다.

{: .v-align-middle}
<img src="Image/Fig2.png"/>

이를 겹쳐보자.

{: .v-align-middle}
<img src="Image/Fig6.PNG"/>

{: .v-align-middle}
<img src="Image/Fig7.PNG"/>

앞서 정의한 것처럼, 어떤 통계값에 대해 특정 결과의 값을 가능성을 함수로서 표현한 것이 되겠다.
예를 들어 x값이 90이라고 가정하였을때, 빨간 선의 y값이 그 가능성을 수치화 한 것이라 볼 수 있다.

### 오즈(Odds)의 이해

{: .v-align-middle}
<img src="Image/Fig11.PNG"/>

Odds는 실패확률 대비 성공확률을 수치로 나타낸 것을 얘기한다. 만일 Odds가 5이라면, 성공확률이 실패확률의 5배라는 의미이다.

### 최대 우도(Maximum Likehood)와 그래프 맞춤(Fitting)

---

## 활성 함수 종류

### 좋은 활성 함수의 조건

좋은 활성 함수의 조건은 다음과 같이 나타낼 수 있다.

* Zero-Centered : 활성 함수 중앙값이 0이여야 한다.
* Symmetric : 활성 함수가 대칭성이 있어야 한다.

### Rectified Linear Unit ( 비선형 연산 )

{: .v-align-middle}
<img src="Image/Fig3.jpg"/>
{: .v-align-middle}
<img src="Image/Fig4.png"/>

모든 0 이하의 입력은 전부 0으로 출력하나, 양수의 입력에선 기울기가 1인 출력을 가진다. 이 함수의 의의는 합성곱 연산 이후 가지는 합성곱의 연산 이후, 이를 오차역전파(Back-Propagation)이라고 한다.

<img src="Image/Fig5.jpg"/>

전통적으로는 이 오차 보정에 Simgoid를 사용하였으나 미분 연산의 용이성, 결정적으로 Gradient가 수행 Layer에 비례해 0으로 수렴하는 Gradient Vanishing 문제로 인해 대체되었다.

