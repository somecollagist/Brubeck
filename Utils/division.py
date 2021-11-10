def div(num, den):
  numerator = [int(x) for x in str(num)]
  denominator = int(den)
  result = []

  try:
    for idx in range(len(numerator)):
      if(denominator > numerator[idx]):
        result.append(0)
        numerator[idx+1] = int(f"{numerator[idx]}{numerator[idx+1]}")
      else:
        result.append(int(numerator[idx]/denominator))
        numerator[idx+1] = int(f"{numerator[idx]%denominator}{numerator[idx+1]}")
  except:
    pass

  return int(str(''.join(str(x) for x in result)))

errs = 0

for x in range(10001):
  print(f"x = {x}")
  for y in range(1, 10001):
    if(div(x, y) != int(x/y)):
      errs += 1
      print(f"Error for {x} / {y}")
      print(f"div() returned {div(x,y)}, result should be {x/y}")
      print()

print(f"Total Errors: {errs}/100,010,000")