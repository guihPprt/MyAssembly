class Compiler {
  constructor(code) {
    this.code_lines = code.split("\n");
    this.sep = "-------------------";
    this.cache = [];
    this.text_cache = [];
    this.block_cache = [];
    this.is_goto = false;
    this.rep_value = 1;
    this.ant_ln = 0;
  }

  compile() {
    let ln = 0;
    let output = "";

    while (ln < this.code_lines.length) {
      let line = this.code_lines[ln];

      try {
        let split = line.split(" ");
        let opcode = split[0];

        if (opcode === "PUT") {
          this.cache.push(split[1]);
        }

        if (opcode === "WRT") {
          let index = parseInt(split[1]) - 1;
          if (index >= 0 && index < this.cache.length) {
            output += this.cache[index] + "\n";
          }
        }

        if (opcode === "SUM") {
          let n1 = parseFloat(this.cache[parseInt(split[1]) - 1]);
          let n2 = parseFloat(this.cache[parseInt(split[2]) - 1]);
          this.cache.push((n1 + n2).toString());
        }

        if (opcode === "SUB") {
          let n1 = parseFloat(this.cache[parseInt(split[1]) - 1]);
          let n2 = parseFloat(this.cache[parseInt(split[2]) - 1]);
          this.cache.push((n1 - n2).toString());
        }

        if (opcode === "DIV") {
          let n1 = parseFloat(this.cache[parseInt(split[1]) - 1]);
          let n2 = parseFloat(this.cache[parseInt(split[2]) - 1]);
          this.cache.push((n1 / n2).toString());
        }

        if (opcode === "MUL") {
          let n1 = parseFloat(this.cache[parseInt(split[1]) - 1]);
          let n2 = parseFloat(this.cache[parseInt(split[2]) - 1]);
          this.cache.push((n1 * n2).toString());
        }

        if (opcode === "{SEP}") {
          output += this.sep + "\n";
        }

        if (opcode === "{CLS}") {
          output = "";
        }

        if (opcode === "T.PUT") {
          if (
            this.code_lines[ln + 1].includes(">") &&
            this.code_lines[ln + 1].includes("<")
          ) {
            this.text_cache.push(
              this.code_lines[ln + 1].replace(">", "").replace("<", "")
            );
          } else {
            output += `Error at line > ${
              ln + 1
            } <\nExpected < to close a string\n`;
            break;
          }
        }

        if (opcode === "T.WRT") {
          let index = parseInt(split[1]) - 1;
          if (index >= 0 && index < this.text_cache.length) {
            output += this.text_cache[index] + "\n";
          }
        }

        if (opcode === "T.INP") {
          if (!this.is_goto) {
            let text = prompt("Enter text:");
            this.text_cache.push(text);
          }
        }

        if (opcode === "INP") {
          if (!this.is_goto) {
            let number = parseFloat(prompt("Enter number:"));
            if (!isNaN(number)) {
              this.cache.push(number.toString());
            } else {
              output += "Please provide a Number\n";
              break;
            }
          }
        }

        if (opcode !== "") {
          if (!this.is_goto && opcode[0] === ":") {
            let pointer = ln;
            let finded = false;
            this.block_cache.push(ln.toString());

            while (!finded) {
              ln++;
              if (this.code_lines[ln].includes("END")) {
                finded = true;
              }
            }
          }
        }

        if (opcode === "END") {
          this.block_cache.push(ln.toString());
          if (this.is_goto) {
            this.is_goto = false;
            ln = this.ant_ln;
          }
        }

        if (opcode === "GOTO") {
          this.ant_ln = ln;
          for (let i = 0; i < this.code_lines.length; i++) {
            if (
              this.code_lines[i].split(" ")[0] === ":" &&
              this.code_lines[i].split(" ")[1] === split[1]
            ) {
              ln = i;
              this.is_goto = true;
            }
          }
        }

        if (opcode === "IF") {
          let value1 = this.cache[parseInt(split[1]) - 1];
          let value2 = this.cache[parseInt(split[3]) - 1];

          switch (split[2]) {
            case "EQ":
              if (value1 === value2) {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[4]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              } else if (split[5] !== "NO") {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[5]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              }
              break;
            case "LT":
              if (parseFloat(value1) < parseFloat(value2)) {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[4]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              } else if (split[5] !== "NO") {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[5]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              }
              break;
            case "MT":
              if (parseFloat(value1) > parseFloat(value2)) {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[4]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              } else if (split[5] !== "NO") {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[5]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              }
              break;
            case "LE":
              if (parseFloat(value1) <= parseFloat(value2)) {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[4]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              } else if (split[5] !== "NO") {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[5]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              }
              break;
            case "ME":
              if (parseFloat(value1) >= parseFloat(value2)) {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[4]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              } else if (split[5] !== "NO") {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[5]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              }
              break;
            case "DIF":
              if (parseFloat(value1) !== parseFloat(value2)) {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[4]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              } else if (split[5] !== "NO") {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[5]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              }
              break;
          }
        }

        if (opcode === "T.IF") {
          let value1 = this.text_cache[parseInt(split[1]) - 1];
          let value2 = this.text_cache[parseInt(split[3]) - 1];

          switch (split[2]) {
            case "EQ":
              if (value1 === value2) {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[4]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              } else if (split[5] !== "NO") {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[5]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              }
              break;
            case "DI":
              if (value1 !== value2) {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[4]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              } else if (split[5] !== "NO") {
                this.ant_ln = ln;
                for (let i = 0; i < this.code_lines.length; i++) {
                  if (
                    this.code_lines[i].split(" ")[0] === ":" &&
                    this.code_lines[i].split(" ")[1] === split[5]
                  ) {
                    ln = i;
                    this.is_goto = true;
                  }
                }
              }
              break;
          }
        }

        if (opcode === "RST") {
          ln = 1;
          this.cache = [];
          this.text_cache = [];
          this.block_cache = [];
        }

        if (opcode === "REP") {
          if (!this.is_goto) {
            if (this.code_lines[ln + 1] !== "CON") {
              output += `Error at line > ${
                ln + 1
              } <\nExpected CON to continue\n`;
              break;
            }
            if (this.rep_value < parseFloat(split[1])) {
              this.ant_ln = ln - 1;
            } else {
              this.ant_ln = ln;
            }
            this.rep_value++;

            for (let i = 0; i < this.code_lines.length; i++) {
              if (
                this.code_lines[i].split(" ")[0] === ":" &&
                this.code_lines[i].split(" ")[1] === split[2]
              ) {
                ln = i;
                this.is_goto = true;
              }
            }
          }
        }

        if (opcode === "CON") {
          this.rep_value = 1;
        }

        ln++;
      } catch (e) {
        output += `Error at line > ${ln} <\n`;
        break;
      }
    }

    return output;
  }
}

function compileCode() {
  const code = document.getElementById("codeInput").value;
  const compiler = new Compiler(code);
  const output = compiler.compile();
  document.getElementById("output").innerText = output;
}

function goto_github() {
  document.location.href = "https://github.com/guihPprt/MyAssembly/tree/main";
}

function goto_home() {
  document.location.href = "index.html";
}

function goto_releases() {
  document.location.href = "https://github.com/guihPprt/MyAssembly/releases";
}
