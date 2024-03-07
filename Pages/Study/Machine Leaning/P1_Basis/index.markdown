---
# Feel free to add content and custom Front Matter to this file.
# To modify the layout, see https://jekyllrb.com/docs/themes/#overriding-theme-defaults

layout: default
title:  Basis
parent: Machine Learning
grand_parent: Study
nav_order: 1
---

# Basis
{: .no_toc }

## Table of contents
{: .no_toc .text-delta }

1. TOC
{:toc}

---

## 개요
 최근 시장에서 AI다, 신경망이다, 딥러닝이다, 이런 의미들이 다소 남용되어 사용되고 있기에 먼저 용어의 정의를 설명 할 필요가 있다고 생각한다.

## 퍼셉트론(Perceptron)

<p align="center">
<img src="Image/Fig7.PNG"/>
</p>

우리 뇌는 수 만 가지의 뉴런(Neuron)의 집합체로 되어있으며, 수 많은 뉴런을 통해 의사 판단을 포함한 여러 가지 뇌 동작을 실현한다.

<p align="center">
<img src="Image/Fig6.PNG"/>
</p>

이것을 공학적인 관점으로 접근하면 비슷한 Model을 구현할 수 있는데 이렇게 구현한 수학적 Model을 퍼셉트론(Perceptron)이라고 부르며 구현 방법에 따라 구조적인 분류법이 달라진다.

<p align="center">
<img src="Image/Fig8.PNG"/>
</p>

퍼셉트론은 사진처럼 3가지로 나누어 설명할 수 있다. 각 단어는 다음과 같이 설명할 수 있다.

* 각 입력값 (x)
* 입력값의 중요도를 부여하는 가중치 (w)
* 입력값을 전부 합산하는 가중값과 그 결과를 상수로서 보정하는 편향값 (b : Bias)
* 이 출력이 유효한 결과인지, 아닌지 판단하는 활성 함수 (Activate Function)

<p align="center">
<img src="Image/Fig9.PNG"/>
</p>

이 퍼셉트론들이 일렬로 줄 지은 것을 층(Layer)라고 부르며, 이 층들의 갯수에 따라 단층, 다층으로 나뉜다.

---

## 심층 학습(Deep Learning)
 하지만 과거 단층, 다층 퍼셉트론으로 구성된 구조적인 문제(Vanishing gradient), 성능 의존적인 문제로 인해 한계가 명확했기에 사장되는 분위기였다. 이후 컴퓨팅 성능의 비약적인 발전 및 끊임없는 연구가 진행되면서 여러 학습 Model을 구현해 볼 수 있었고 딥 러닝은 그 중 하나이다.

 공학적인 정의의 딥 러닝은 입력층과 출력층을 제외한 은닉층이 2층 이상으로 이를 계속 학습시키도록 하는 것을 칭한다. 하지만, 좀 더 실용적인 측면에서의 정의는 "어떠한 데이터에서 사용자가 원하는 특징을 찾아 낼 수 있도록 Model에게 반복적인 학습을 시키는 것"을 의미한다.

 보통 딥 러닝에는

* 합성곱 신경망 (CNN : Convolutional Neural Network)
* 순환 신경망 (RNN : Recurrent Neural Network)
* 트랜스포머 (Transformer)

상기 3가지의 알고리즘을 사용하고 있다.

## Convoltion ( 합성곱 )

<p align="center">
<img src="Image/Fig1.gif"/>
</p>

합성곱은 정석적인 의미로서는 서로 다른 두 함수의 연산에서 세번째 함수가 수치적 형태로 나오는 것을 의미한다. 다만, 앞서 설명한 CNN에서는 이를 입력 값이 필터에 부합하는 특징을 가지고 있는지 판별하기 위해 사용한다. 이 특징을 출력하는 Data set을 "특징 맵(Feature map)"이라고 한다.

 ---

## Pooling ( 풀링 )

 <p align="center">
<img src="Image/Fig2.PNG"/>
</p>

풀링은 입력에서 행렬의 특정 범위에서, 어떤 조건을 가진 값만을 취하는 것을 말한다. 사진의 예로, 좌측 4x4 입력 행렬에 대한 2x2, Stride 2를 가지는 최대 풀링 연산을 하였을 때 우측 2x2 행렬과 같은 결과값을 도출 할 수 있다.
