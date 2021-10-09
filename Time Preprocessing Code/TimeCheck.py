#!/usr/bin/env python
# coding: utf-8

# In[30]:


def erase_empty_json(json):
    n_json = []
    for i in json['peekTime']:
        if i != '':
            n_json.append(i)
            
    return n_json


# In[31]:


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


# In[32]:


def Slicing_Json_Time(time):
    return time[11:-3]


# In[38]:


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

        #print(x)
        #print(y)
        #print('------------')

        ans1.append(x)
        ans2.append(y)
    return ans1, ans2


# In[39]:


def slice_last_json(json_data):
#     ans1 = []
#     ans2 = []

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

    #print(x)
    #print(y)
    #print('------------')

    ans1 = x
    ans2 = y
    return ans1, ans2


# In[34]:


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


# In[35]:


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


# In[36]:


def loop_for_time(start, end, json_start, json_end, raw_time):
    left = 0
    ans = [[],[]]
    for i in range(start,end):
        q = Loop_For_Checking_Time(json_start[i], raw_time.values)
        w = Loop_For_Checking_Time(json_end[i], raw_time.values)
        left += w - q
        ans[0].append(q)
        ans[1].append(w)
        #print('go :: %d' %q)
        #print('to :: %d' %w)
        #print('diff :: %d' %(w - q))
        #print('------------')
        
    
    return ans, left/len(ans[0])
        
if __name__ == "__main__":
    aaa = ['2021-06-13 19:56:59.990 PM']
    Slicing_Json_Time(aaa)


# left = 0
# st = 2
# v = 11
# for i in range(st,v):
#     q = Loop_For_Checking_Time(ans1[i], t_fp1.values)
#     w = Loop_For_Checking_Time(ans2[i], t_fp1.values)
#     print('go :: %d' %q)
#     print('to :: %d' %w)
#     print('diff :: %d' %(w - q))
#     left += w - q
#     print('------------')

# print(left/v)
# print(Loop_For_Checking_Time(js, t_fp1.values))
# print(Loop_For_Checking_Time(js2, t_fp1.values))
# a = Loop_For_Checking_Time(js2, t_fp1.values) - Loop_For_Checking_Time(js, t_fp1.values)
# print(a)

# b += a


# %%
