import logic

msg = """'+' => "UAO",
'*' => "UAU",
'&' => "UEA",
'/' => "UEE",
'@' => "UEI",
'|' => "UEO",
'^' => "UEU",
'£' => "UIA","""

out = ""
for ln in msg.split("\n"):
    out += f"'{ln[1].lower()}' => \"{logic.NOT(ln[-5:-2])}\",\n"

print(out)