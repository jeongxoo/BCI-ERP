#!/usr/bin/env python
# coding: utf-8

# In[2]:


import pandas as pd
import json


# In[115]:


temp = pd.read_csv('0606_민재/Rawdata5.txt',delimiter='\t',encoding='cp949')
t_fp1 = temp['Time']


# In[116]:


with open('박민재 time/5.json', 'r') as f:
    json_data = json.load(f)

print(json.dumps(json_data, indent="\t") )


# In[117]:


print(json_data['peekTime'])
n_json_data = []
for i in json_data['peekTime']:
    if i != '':
        n_json_data.append(i)


# In[118]:


print(n_json_data)


# In[119]:


print(len(t_fp1[0]))
print(t_fp1)


# In[120]:


# print(jsjs)
ans1 = []
ans2 = []
for i in n_json_data:
    print(Slicing_Json_Time(i))
    a = Slicing_Json_Time(i)[:6]
    b = Slicing_Json_Time(i)[6:]
    c = float(b) - 0.2
    d = float(b) + 1
#     print(a)
#     print(c,d)

    if c < 10:
        c = format(c, ".3f")
        x = a + '0' + str(c)
    else:
        c = format(c, ".3f")
        x = a + str(c)
        
    if d < 10:
        d = format(d, ".3f")
        y = a + '0' + str(d)
    else:
        d = format(d, ".3f")
        y = a + str(d)

    print(x)
    print(y)
    print('------------')
    
    ans1.append(x)
    ans2.append(y)


# In[121]:


print(ans1)
print(ans2)


# In[129]:


left = 0
st = 2
v = 11
for i in range(st,v):
    q = Loop_For_Checking_Time(ans1[i], t_fp1.values)
    w = Loop_For_Checking_Time(ans2[i], t_fp1.values)
    print('go :: %d' %q)
    print('to :: %d' %w)
    print('diff :: %d' %(w - q))
    left += w - q
    print('------------')

print(left/v)
# print(Loop_For_Checking_Time(js, t_fp1.values))
# print(Loop_For_Checking_Time(js2, t_fp1.values))
# a = Loop_For_Checking_Time(js2, t_fp1.values) - Loop_For_Checking_Time(js, t_fp1.values)
# print(a)

# b += a


# In[10]:


def Slicing_Mave_Time(time):
    if time[:2] == '오전':
        n_time = time[3:]
    else:
        if time[3:5] == '12':
            n_time = time[3:-1]
        else:
            hour = int(time[3:4]) + 12
            n_time = str(hour) + time[4:-1]
    
    return n_time


# In[11]:


def Slicing_Json_Time(time):
    return time[11:-3]


# In[12]:


def Compare_Json_Mave(json_t, mave_t):
    if len(json_t) != len(mave_t):
        return False
    else:
        for i in range(len(json_t)):
            if json_t[i] != mave_t[i]:
                return False
            else:
                if i == len(json_t) - 3:
                    return True


# In[13]:


def Loop_For_Checking_Time(json, mave):
#     n_json = Slicing_Json_Time(json)
#     print(len(n_json))
    n_json = json
    check = False
    n = 0
    
    while(check == False):
        n -= 1
        n_mave = Slicing_Mave_Time(mave[n])
#         print(len(n_mave))
        check = Compare_Json_Mave(n_json, n_mave)
        
    n = n + len(mave)
    return n


# In[71]:


print(Loop_For_Checking_Time(js, t_fp1.values))
print(Loop_For_Checking_Time(js2, t_fp1.values))


# In[ ]:




