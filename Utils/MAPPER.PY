import logic

msg = """'A' => new("IIO"),
'B' => new("IIU"),
'C' => new("IOA"),
'D' => new("IOE"),
'E' => new("IOI"),
'F' => new("IOO"),
'G' => new("IOU"),
'H' => new("IUA"),
'I' => new("IUE"),
'J' => new("IUI"),
'K' => new("IUO"),
'L' => new("IUU"),
'M' => new("OAA"),
'N' => new("OAE"),
'O' => new("OAI"),
'P' => new("OAO"),
'Q' => new("OAU"),
'R' => new("OEA"),
'S' => new("OEE"),
'T' => new("OEI"),
'U' => new("OEO"),
'V' => new("OEU"),
'W' => new("OIA"),
'X' => new("OIE"),
'Y' => new("OII"),
'Z' => new("OIO"),"""

out = ""
for ln in msg.split("\n"):
    out += f"'{ln[1].lower()}' => new(\"{logic.NOT(ln[-6:-3])}\"),\n"

print(out)