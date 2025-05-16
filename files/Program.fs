open System.IO
open System
let mensaje = ["Esta es una demostracion";"de como grabar multiples lineas";"en un archivo"]

//
// Escribiendo las lineas en el archivo en una sola operacion
//
File.WriteAllLines("demo1.txt",mensaje)

//
// Aqui preguntamos linea por linea y luego la escribimo
//
let lineas =
    Seq.initInfinite (
        fun _ ->
            printfn "Entre nombre, SALIR para salir"
            Console.ReadLine()
    )
    |> Seq.takeWhile (fun e -> e <> "SALIR")
    |> Seq.toList



File.WriteAllLines("demo2.txt",lineas)


//
// Leyendo las lineas a ver si quedaron
// bien.
//
let test = File.ReadAllLines("leo.txt")
printfn $"{test.Length}"


