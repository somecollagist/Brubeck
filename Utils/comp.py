from enum import Enum, IntFlag

class Qit(Enum):
    A = -2,
    E = -1,
    I = 0,
    O = 1,
    U = 2

Z = Qit.I
C = Qit.I

class CompState(IntFlag):
    NN = 0b0000
    EQ = 0b1000
    ZO = 0b0100
    CM = 0b0010
    LT = 0b0001

    def Yield():
        ret = CompState.NN

        ret |= (CompState.EQ if Z == Qit.O or Z == Qit.U else CompState.NN)
        ret |= (CompState.ZO if Z == Qit.E or Z == Qit.U else CompState.NN)
        ret |= (CompState.CM if C == Qit.A or C == Qit.U else CompState.NN)
        ret |= (CompState.LT if C == Qit.A or C == Qit.E else CompState.NN)

        return ret

for z in [Qit.A, Qit.E, Qit.O, Qit.U]:
    Z = z
    for c in [Qit.A, Qit.E, Qit.O, Qit.U]:
        C = c

        print(f"Z = {Z}, C = {C} :: 0b{'{0:04b}'.format(CompState.Yield())}")