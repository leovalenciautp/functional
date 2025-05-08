open System.Net.Http
open System.Text.Json

let getURLAsync (url:string) =
    task {
        use client = new HttpClient()
        let! response = client.GetAsync url
        let! body = response.Content.ReadAsStringAsync()
        return body
    }

let getURL = getURLAsync >> Async.AwaitTask >> Async.RunSynchronously

//
// Obtener libros del Project Gutemberg
//
let demoUrl = "https://gutendex.com/books?search=Pride"

let result = getURL demoUrl
printfn $"{result}"