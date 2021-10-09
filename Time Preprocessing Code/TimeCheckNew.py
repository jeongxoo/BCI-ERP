#!/usr/bin/env python
# coding: utf-8

# In[1]:


def erase_empty_json(json):
    n_json = []
    for i in json['peekTime']:
        if i != '':
            n_json.append(i)
            
    return n_json


# In[2]:


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


# In[3]:


def Slicing_Json_Time(time):
    return time[11:-3]


# In[4]:


def slice_json(json_data):
    ans1 = []
    ans2 = []

    for i in json_data:
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

#         print(x)
#         print(y)
#         print('------------')

        ans1.append(x)
        ans2.append(y)
    return ans1, ans2


# In[9]:


def slice_last_json(json_data):
#     ans1 = []
#     ans2 = []

    a = Slicing_Json_Time(i)[:6]
    b = Slicing_Json_Time(i)[6:]
    
    c = float(b)
    d = float(b) + 1
#     print(a)
#     print(c,d)

    if c < 10 and c >= 0:
        c = format(c, ".3f")
        x = a + '0' + str(c)
    elif c < 0:
        # 분이 바뀌는 경우
        print(c)
    else:
        c = format(c, ".3f")
        x = a + str(c)

    if d < 10:
        d = format(d, ".3f")
        y = a + '0' + str(d)
    elif c >= 60:
        print(d)
    else:
        d = format(d, ".3f")
        y = a + str(d)

#     print(x)
#     print(y)
#     print('------------')

    ans1 = x
    ans2 = y
    return ans1, ans2


# In[18]:


def slice_json_to04(json_data):
    ans1 = []
    ans2 = []

    for i in json_data:
#         print(i)
        a = Slicing_Json_Time(i)[:6]
        b = Slicing_Json_Time(i)[6:]
#         print(a)
#         print(b)
        c = float(b)
        d = float(b) + 0.4
        
        e = Slicing_Json_Time(i)[:3]
        f = Slicing_Json_Time(i)[3:5]
        g = int(f) + 1
        
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
        elif d >= 60:
            d = d - 60.0
            d = format(d, ".3f")
            d = '0' + str(d)
            y = e + str(g) + ':' + d
        else:
            d = format(d, ".3f")
            y = a + str(d)

#         print(x)
#         print(y)
        print('------------')

        ans1.append(x)
        ans2.append(y)
    return ans1, ans2


# In[6]:


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


# In[6]:


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


# In[7]:


def loop_for_time(start, end, json_start, json_end, raw_time):
    left = 0
    ans = [[],[]]
    for i in range(start,end):
        q = Loop_For_Checking_Time(json_start[i], raw_time.values)
        w = Loop_For_Checking_Time(json_end[i], raw_time.values)
        left += w - q
        ans[0].append(q)
        ans[1].append(w)
        print('------------')
        
    
    return ans, left/len(ans[0])


# In[24]:

if __name__ == "__main__":
    aaa = ['2021-06-13 19:56:59.990 PM']
    slice_json_to04(aaa)


# In[ ]:




