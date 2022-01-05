#-*- coding:utf-8 -*-
#导入需要的模块
import pandas as pd
import os
import sys
#AES加密
from Cryptodome.Cipher import AES
import base64

AES_KEY = "1234567890123456" 
AES_IV = "zombiestestIV123"
BLOCK_SIZE = 16  # Bytes

pad = lambda data: data + (BLOCK_SIZE - len(data.encode('utf-8')) % BLOCK_SIZE) * \
                chr(BLOCK_SIZE - len(data.encode('utf-8')) % BLOCK_SIZE)
                
unpad = lambda s: s[:-ord(s[len(s) - 1:])]

# str不是32的倍数那就补足为16的倍数
def add_to_32(value):
    while len(value) % 32 != 0:
        value += '\0'
    return str.encode(value)  # 返回bytes
    
# str不是16的倍数那就补足为16的倍数
def add_to_16(value):
    while len(value) % 16 != 0:
        value += '\0'
    return str.encode(value)  # 返回bytes
    
def aesEncrypt(key, IV, text):
    obj = AES.new(key.encode('utf-8'), AES.MODE_CBC, IV.encode('utf-8'))
    text = pad(text)
    ciphertext = obj.encrypt(text.encode('utf-8'))
    enctext = base64.b64encode(ciphertext).decode('utf-8')
    return enctext
    
curpath = os.path.dirname(os.path.abspath(sys.argv[0]))

#单个文件Excel转换成Csv函数， excel_file是Excel文件名，csv_file是转换成的Csv文件名
def excel2csv(excel_file,csv_file):   
    print('[file %s->%s]' % (excel_file,csv_file))
    data_xls=pd.read_excel(excel_file,sheet_name=0)
    data_xls.to_csv(csv_file,encoding='utf-8',index=False)

#单个文件Excel转换成加密Csv函数
def excel2csv_encrypt(excel_file,csv_file):   
    print('[file %s->%s]' % (excel_file,csv_file))
    data_xls=pd.read_excel(excel_file,sheet_name=0)
    data_xls.to_csv(csv_file,encoding='utf-8',index=False)
    #aes begin====
    f = open(csv_file, mode='r', encoding='UTF-8')
    s = f.read()
    f.close()
    s = aesEncrypt(AES_KEY, AES_IV, s)
    write_file(s, csv_file)
    #aes end====
    
#读取一个指定目录里的所有文件
def read_path(path):
    dirs=os.listdir(path)
    return dirs

#删除指定目录里的所有文件
def del_file(path):
    ls=os.listdir(path)
    for i in ls:
        p_path=os.path.join(path,i)       
        if os.path.isdir(p_path):
             del_file(p_path)
        else:
            os.remove(p_path)

#写文件
def write_file(content, file):
    with open(file, mode='w', encoding='UTF-8') as f:
        f.write(content)
    
#主函数
#source是Excel路径，csvpath是转换的csv文件路径, csvPath_Encrypt是AES加密后的csv文件路径
def main(source, csvPath, csvPath_Encrypt, localizaPath):

    print('[path %s->%s]' % (source,csvPath))
    del_file(csvPath) 
    del_file(csvPath_Encrypt)
    file_list=[source+'\\'+i for i in read_path(source)]
    j=1
    #遍历指定路径里的左右文件并执行excel2csv转换函数
    for it in file_list:        
        file_path=os.path.split(it)[1]
        file_name=os.path.splitext(file_path)[0]   
        if file_name.find('~$') != -1:
            continue
        if file_name=='Localization':           
            csv_file=localizaPath+'\\'+file_name+".csv"   
            if os.path.exists(csv_file):
                os.remove(csv_file)
            excel2csv(it,csv_file)
        else:
            csv_file=csvPath+'\\'+file_name+".csv"       
            csv_file_encrypt=csvPath_Encrypt+'\\'+file_name+".csv"
            excel2csv(it,csv_file)   
            excel2csv_encrypt(it,csv_file_encrypt)
        j=j+1

if __name__=='__main__':

    if len(sys.argv) < 4:
        print('python excel2csv.py <excel_input_path> <lua_output_path>')
        exit(1)

 #获取批处理参数，执行转换
    main(os.path.join(curpath,sys.argv[1]),os.path.join(curpath,sys.argv[2]),os.path.join(curpath,sys.argv[3]),os.path.join(curpath,sys.argv[4]))
    print('\n-----转换完成-----')
    exit(0) 
