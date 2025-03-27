//
// Convertir de UVT a pesos
//

let uvtFactor = 47065.0
let uvtToPesos x =
    x*uvtFactor
let pesosToUvt x =
    x/uvtFactor

type TaxBrackets =
    {
        RangoBajo: float
        RangoAlto: float
        Impuesto: float
        Base: float
    }

let dianTable =
    [
        {
            RangoBajo = 0.0
            RangoAlto = 1090.0
            Impuesto = 0.0
            Base = 0.0
        }
        {
            RangoBajo = 1090.0
            RangoAlto = 1700.0
            Impuesto = 0.19
            Base = 0.0
        }
        {
            RangoBajo = 1700.0
            RangoAlto = 4100.0
            Impuesto = 0.28
            Base = 116.0
        }
        {
            RangoBajo = 4100.0
            RangoAlto = 8670.0
            Impuesto = 0.33
            Base = 788.0
        }
        {
            RangoBajo = 8670.0
            RangoAlto = 18970.0
            Impuesto = 0.35
            Base = 2296.0
        }
        {
            RangoBajo = 18970.0
            RangoAlto = 31000.0
            Impuesto = 0.37
            Base = 5901.0
        }
        {
            RangoBajo = 31000.0
            RangoAlto = 999999.0
            Impuesto = 0.39
            Base = 10352.0
        }

    ]

//
// Esta funcion busca en la tabla, comparando
// la uvt con el RangoBajo y el RangoAlto
//
let findTaxBracket uvt =
    dianTable
    |> List.find 
        (fun bracket 
            ->
            uvt >= bracket.RangoBajo && uvt < bracket.RangoAlto
        )


let calculateTax uvt bracket =
    (uvt-bracket.RangoBajo)*bracket.Impuesto+bracket.Base

let calcularImpuesto uvt =
    uvt
    |> findTaxBracket
    |> calculateTax uvt
let salario = 30000000.0*12.0


let tax =
    salario
    |> pesosToUvt
    |> calcularImpuesto
    |> uvtToPesos

printfn $"Total a pagar {tax}"
