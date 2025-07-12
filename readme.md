# 🐾 MeowLang++  

Inspired by Brainf\*\*k, MeowLang++ uses a minimal syntax of feline-friendly tokens to manipulate a memory tape. All programs are made entirely from words like `meow`, `miau`, `:3`, and `nya~`.

---

## 🐱 Features
- Tape-based memory model (like Brainf\*\*k)
- Stack for pointer manipulation
- ASCII and numeric output
- Looping constructs with `nya`/`nya~`
- Pure functional interpreter written in F#
- Supports comments with `#`
- Zero-dependency console app

---

## 🧰 Requirements
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

---

## 🛠️ Build
```bash
dotnet build -c Release
```

---

## 🚀 Run
```bash
./meowlang.exe path/to/yourfile.meow
```

---

## 🐈 Language Syntax

| Keyword  | Meaning                            |
|----------|------------------------------------|
| `meow`   | Move pointer right (`ptr += 1`)    |
| `mreow`  | Move pointer left (`ptr -= 1`)     |
| `miau`   | Increment current cell (`mem[ptr] += 1`) |
| `mlem`   | Decrement current cell (`mem[ptr] -= 1`) |
| `purr`   | Output current cell as character   |
| `>:3`    | Output current cell as number      |
| `>:3c`   | Zero current cell (`mem[ptr] = 0`) |
| `:3`     | Push current pointer to stack      |
| `:3c`    | Pop pointer from stack             |
| `nya`    | Begin loop (executes while `mem[ptr] != 0`) |
| `nya~`   | End loop                           |

> ⚠️ Loops support nesting. `:3c` will fail if used with an empty stack.

---

## 👋 Example: Hello World

### `Examples/hello.meow`
```meow
# Set first cell to 72 (ASCII 'H')
miau miau miau miau miau miau miau miau miau   # 9
miau miau miau miau miau miau miau miau miau   # 18
miau miau miau miau miau miau miau miau miau   # 27
miau miau miau miau miau miau miau miau miau   # 36
miau miau miau miau miau miau miau miau miau   # 45
miau miau miau miau miau miau miau miau miau   # 54
miau miau miau miau miau miau miau miau miau   # 63
miau miau miau miau miau miau miau miau miau   # 72
purr

# Move to next cell
meow

# Set second cell to 73 (ASCII 'I')
miau miau miau miau miau miau miau miau miau   # 9
miau                                           # 10
miau miau miau miau miau miau miau miau miau   # 19
miau miau miau miau miau miau miau miau miau   # 28
miau miau miau miau miau miau miau miau miau   # 37
miau miau miau miau miau miau miau miau miau   # 46
miau miau miau miau miau miau miau miau miau   # 55
miau miau miau miau miau miau miau miau miau   # 64
miau miau miau miau miau miau miau miau miau   # 73
purr
```

Output:
```
HI
```

---

## 🗃️ Project Files
- `Program.fs` – Core compiler/interpreter (F# functional implementation)
- `Examples/` – Folder of `.meow` programs
  - `hello.meow` – Hello World example
- `README.md` – This documentation

---

## ⚖️ License
MIT License

---
> Nya~ on!
