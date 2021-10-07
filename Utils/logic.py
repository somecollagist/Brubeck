import numpy

reference = [[-1, -1,  0,  0,  0],
             [-1,  0,  0,  0,  0],
             [ 0,  0,  0,  0,  0],
             [ 0,  0,  0,  0,  1],
             [ 0,  0,  0,  1,  1]]

build =     [[0, 0, 0, 0, 0],
             [0, 0, 0, 0, 0],
             [0, 0, 0, 0, 0],
             [0, 0, 0, 0, 0],
             [0, 0, 0, 0, 0]]

def NOT(a):
    return -a

def AND(a, b):
    if a < b: return a
    else: return b

def OR(a, b):
    if a > b: return a
    else: return b

def XOR(a,b):
    return int((-1*a*b)/2)

def INC(a):
    if a == 2:
        return -2
    else:
        return a+1

def DEC(a):
    if a == -2:
        return 2
    else:
        return a-1

def GEN(a):
    while a > 2:
        a -= 5
    while a < -2:
        a += 5
    return a

for x in range(-2,3):
    for y in range(-2,3):
        build[x+2][y+2] = int((AND(x,y) + OR(x,y))/3)

print(numpy.array(reference))
print()
print(numpy.array(build))
print("\n")
print(numpy.array(build) == numpy.array(reference))
