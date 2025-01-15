# API

The language has two basic types, which are numbers and text. Similarly, the language is also divided into these two types, meaning text does not work with numbers.

## Code blocks

Code blocks are used to execute necessary commands, such as an `IF` or a `GOTO` statement.

We use `:` to define the beginning of a block, followed by its name, and then we end the block with `END`. Inside the block, we write what we want to be executed when called.

![alt text](image-8.png)

The `GOTO` command is used to call a code block.

## Number

### 1. Add a number to memory

The memory is divided into 3 files, which can only be accessed by the main directory of the compiler. This was done to facilitate the analysis of the code and its results.

These files are:
<br>
<code>cache.rom</code>
<code>text_cache.rom</code>
<code>block_cache.rom</code>

1. To add numbers to memory, we use this code:

   ![alt text](image.png)

   Numbers are stored in memory addresses, and they are allocated according to the order in which they are placed in the files.

   ![alt text](image-1.png)

### 2. Show a number in the console

1. To show a number in the console, you need to add it to memory first. Then, retrieve it from memory and display it using the `WRT` command.

   ![alt text](image-2.png)
   ![alt text](image-3.png)

   We can handle multiple numbers in memory and display them in the order we want.

   ![alt text](image-4.png)
   ![alt text](image-5.png)

### 3. Handle user inputs

1. To handle user inputs, we use the `INP` command. It is placed in the same way as the `PUT` command, meaning its address depends on the position it was placed in the code.

   ![alt text](image-6.png)
   ![alt text](image-7.png)

### 4. Conditions

1. Antes de verificar uma condicao primeiro precisamos definir blocos de codigo, esses blocos de codigos vao ser a acao que vai ser realizada se a condicao e verdadeira ou falsa

   ![alt text](image-9.png)

   Unlike most languages, the `IF` statement has a more distinct syntax.

   ![alt text](image-10.png)

   ![alt text](image-11.png)

   ![alt text](image-12.png)
