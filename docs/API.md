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

1. Before checking a condition, we first need to define code blocks. These code blocks will be the actions that will be performed if the condition is true or false.

   ![alt text](image-9.png)

   Unlike most languages, the `IF` statement has a more distinct syntax.

   ![alt text](image-10.png)

   ![alt text](image-11.png)

   ![alt text](image-12.png)

## Text

### 1. Add text to memory

1. To add a text to memory we use the code `T.PUT`

   ![alt text](image-13.png)

   You need to place the text between `><` on the line below the `T.PUT`

### 2. Show text in the console

1. To show text in the console, you need to add it to memory first. Then, retrieve it from memory and display it using the `T.WRT` command.

   ![alt text](image-14.png)
   ![alt text](image-15.png)

   We can handle multiple texts in memory and display them in the order we want.

   ![alt text](image-16.png)
   ![alt text](image-17.png)

### 3. Handle user inputs

1. To handle user inputs, we use the `T.INP` command. It is placed in the same way as the `T.PUT` command, meaning its address depends on the position it was placed in the code.

   ![alt text](image-19.png)
   ![alt text](image-18.png)

### 4. Conditions

### 4. Conditions

1. Conditions work the same way as number conditions.

   ![alt text](image-20.png)

   The only difference is using `T.IF` instead of `IF`.

   ![alt text](image-21.png)

## Other statement

<code>{SEP}</code> : Add a separator in output

![alt text](image-22.png)

<code>{CLS}</code> : Clear the output

<code>GOTO</code> : Go to the block code named

![alt text](image-24.png)

<code>REP</code> : Repeat the block code n of times

![alt text](image-25.png)
