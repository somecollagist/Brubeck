import math

CharToInt = {
    "A" : -2,
    "E" : -1,
    "I" :  0,
    "O" :  1,
    "U" :  2
}

IntToChar = {
    -2 : "A",
    -1 : "E",
     0 : "I",
     1 : "O",
     2 : "U",
}

## IIIII OEIAE = 489

def CnvCharsToInt(qits : str):
    total = 0
    for x in range(len(qits)):
        total += CharToInt[qits[len(qits) - 1 - x]] * (5 ** x)
    return total

def CnvIntToChars(num: int):
    size = 10
    chars = ['I' for x in range(size)]

    if num != 0:
        delta = 1
        if num < 0:
            num *= -1
            delta = -1

        idx = int(math.log(2*num, 5))
        while num != 0:
            coef = GetClosestToZero(num, idx)
            num -= coef * (5 ** idx)
            chars[idx] = IntToChar[coef * delta]
            idx -= 1

    return ''.join(str(x) for x in chars[::-1])

def GetClosestToZero(num : int, power : int):
    scores = {
        -2 : 0,
        -1 : 0,
         0 : 0,
         1 : 0,
         2 : 0
    }
    
    for score in scores:
        scores[score] = abs(num - (score * (5 ** power)))

    ret = scores[-2]
    val = -2
    for score in scores:
        if scores[score] == 0:
            return score
        if scores[score] < ret:
            ret = scores[score]
            val = score
    
    return val

