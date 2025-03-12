/
// Convertir de UVT a pesos
//

let uvtFactor = 47065.0
let uvtToPesos x =
    x*uvtFactor
let pesosToUvt x =
    x/uvtFactor

let dianTable =
    [
        (0.0,1090.0,0.0,0.0)
        (1090.0,1700.0,0.19,0.0)
        (1700.0,4100.0,0.28,116)
        (4100.0,8670.0,0.33,788)
        (8670.0,18970.0,0.35,2296)
        (18970.0,31000.0,0.37,5901)
        (31000.0,99999.0,0.39,10352)
    ]

//
// Esta funcion busca en la tabla
let findTax uvt =
    dianTable
    |> List.find 
        (fun (l,h,_,_) 
            ->
            (uvt >= l) && (uvt < h)
        )


let calculateTax uvt (l,_,tax,b) =
    (uvt-l)*tax+b

let salario = 1_500_000.0*12.0

let uvts = 
    salario
    |> pesosToUvt

let tax =
    uvts
    |> findTax
    |> calculateTax uvts
    |> uvtToPesos

printfn $"Total a pagar {tax}"
