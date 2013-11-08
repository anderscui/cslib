#light
open System


let count() = 
    let next = ref 0
    let inc() =
        let v = next
        next := !next + 1
        !v
        
    inc

let inc1 = count()
//printf "%A" inc1()
Console.WriteLine(inc1())
Console.WriteLine(inc1())
Console.WriteLine(inc1())

Console.ReadLine() |> ignore