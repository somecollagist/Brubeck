import numpy

reference = [[ 0,  2,  0, -2,  0],
             [ 0,  0,  0,  0,  0],
             [ 0,  0,  0,  0,  0],
             [ 0,  0,  0,  0,  0],
             [ 0, -2,  0,  2,  0]]

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
    return int(a)

for x in range(-2,3):
    for y in range(-2,3):
        try:
            build[x+2][y+2] = GEN(((2*x)/(3*y)))*2
        except:
            build[x+2][y+2] = 0
        ##build[x+2][y+2] = GEN(build[x+2][y+2])

# for x in range(5):
#     build[2][x] = 3

print(numpy.array(reference))
print()
print(numpy.array(build))
print("\n")
print(numpy.array(build) == numpy.array(reference))
