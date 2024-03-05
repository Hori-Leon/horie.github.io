---
# Feel free to add content and custom Front Matter to this file.
# To modify the layout, see https://jekyllrb.com/docs/themes/#overriding-theme-defaults

layout: default
title:  Basis
parent: Deep leanring
has_children: false
---

# Basis
{: .no_toc }

## Table of contents
{: .no_toc .text-delta }

1. TOC
{:toc}

---

## Convoltion ( 합성곱 )

{: .v-align-middle}
<img src="Image/Fig1.gif"/>

합성곱은 서로 다른 두 함수의 연산에서 세번째 함수가 수치적 형태로 나오는 것을 의미하나, Deep learning에서는 입력 값이 필터에 부합하는 특징을 가지고 있는지 판별하기 위해 사용한다.

 ---

## Pooling ( 풀링 )
 
 {: .v-align-middle}
<img src="Image/Fig2.PNG"/>

풀링은 입력에서 행렬의 특정 범위에서, 어떤 조건을 가진 값만을 취하는 것을 말한다. 사진의 예로, 좌측 4x4 입력 행렬에 대한 2x2, Stride 2를 가지는 최대 풀링 연산을 하였을 때 우측 2x2 행렬과 같은 결과값을 도출 할 수 있다.

---

## Rectified Linear Unit ( 비선형 연산 )

{: .v-align-middle}
<img src="Image/Fig3.jpg"/>
{: .v-align-middle}
<img src="Image/Fig4.png"/>

모든 0 이하의 입력은 전부 0으로 출력하나, 양수의 입력에선 기울기가 1인 출력을 가진다. 이 함수의 의의는 합성곱 연산 이후 가지는 합성곱의 연산 이후, 이를 오차역전파(Back-Propagation)이라고 한다.

<img src="Image/Fig5.jpg"/>

전통적으로는 이 오차 보정에 Simgoid를 사용하였으나 미분 연산의 용이성, 결정적으로 Gradient가 수행 Layer에 비례해 0으로 수렴하는 Gradient Vanishing 문제로 인해 대체되었다.

Activate Function에 대한 설명은 따로 작성할 예정


 