#!/usr/bin/env python
# coding: utf-8

# In[72]:


#신경공학 P300 ( 자극 제시후, 250ms ~ 350ms ), N200 ( 자극 제시후, 100ms ~ 200ms )라고 가정,
#x_data = (n200구간의 최소값, p300 구간의 최대값) 으로 하면 어떨까

import numpy as np


# In[74]:


def sigmoid(x):
    return 1 / (1+np.exp(-x))


# In[75]:

#손실함수 구현
def loss_func(x, t,W,b):
    delta = 1e-7 #log 무한대 방지
    
    z = np.dot(x,W) + b
    y = sigmoid(z)
    #cross entropy
    return -np.sum(t*np.log(y + delta) + (1-t)*np.log((1-y)+delta))


# In[76]:

#미분함수 구현
def numerical_derivative(f, x):
    delta_x = 1e-4 # 0.0001
    grad = np.zeros_like(x)
    
    it = np.nditer(x, flags=['multi_index'], op_flags=['readwrite'])
    
    while not it.finished:
        idx = it.multi_index        
        tmp_val = x[idx]
        x[idx] = float(tmp_val) + delta_x
        fx1 = f(x) # f(x+delta_x)
        
        x[idx] = tmp_val - delta_x 
        fx2 = f(x) # f(x-delta_x)
        grad[idx] = (fx1 - fx2) / (2*delta_x)
        
        x[idx] = tmp_val 
        it.iternext()   
        
    return grad


# In[83]:


# 손실함수 값 계산 함수
# 입력변수 x, t : numpy type
def error_val(x, t,W,b):
    delta = 1e-7 #log 무한대 방지
    z = np.dot(x,W) + b
    y = sigmoid(z)
    
    return -np.sum(t*np.log(y+delta) + (1-t)*np.log((1-y)+delta))

# 학습을 마친 후, 임의의 데이터에 대해 미래 값 예측 함수
# 입력변수 x : numpy type
def predict(x,W,b):
    z = np.dot(x,W) + b
    y = sigmoid(z)
    if x[0]*x[1]>0:
        result=0
        return y, result
    else:
    #     if y>0.5:
        if y<=0.55413 and y>=0.554122:
            result = 1
        else:
            result = 0
        return y, result


# In[78]:


# learning_rate = 1e-6  # 발산하는 경우, 1e-2, 1e-3, 1e-6 등으로 바꾸어 실행
# f = lambda x : loss_func(x_data,t_data)

# print("Initial error value = ", error_val(x_data, t_data), "Initial W = ", W, "\n", ", b = ", b )

# for step in  range(100001):  
    
#     W -= learning_rate * numerical_derivative(f, W)
    
#     b -= learning_rate * numerical_derivative(f, b)
    
#     if (step % 600 == 0):
#         print("step = ", step, "error value = ", error_val(x_data, t_data), "W = ", W, ", b = ",b )





