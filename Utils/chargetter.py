def encode(msg):
    out = ""
    for c in msg:
            o = c
            if o == " ":
                    o = "SPC"
            elif o == ".":
                    o = "STOP"
            elif o == ",":
                    o = "COMMA"
            out += f"Char.{o}, "
    print(out)
    print(f"length: {len(msg)}")

encode("Hello world!")