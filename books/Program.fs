open System.Net.Http
open System.Text.Json


type Person = {
  birth_year: int option
  death_year: int option
  name: string
}

type Book = {
    id: int
    title: string
    subjects: string array
    authors: Person array
    summaries: string array
    translators: Person array
    bookshelves: string array
    languages: string array
    copyright: bool option
    media_type: string
    formats: Map<string,string>
    download_count: int
}

type Answer = {
  count: int
  next: string option
  previous: string option
  results: Book array
}

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
let demoUrl = @"https://gutendex.com/books?search=Verne%20Moon"

let result = getURL demoUrl

//
// Deserialize the JSON
//
let libros = JsonSerializer.Deserialize<Answer> result

//
// Si funciona debemos tener algo de informacion aqui
//
printfn $"Numero de libros: {libros.count}"

printfn "----Titulos----"
//
// Vamos a imprimir el titulo de los libros encontrados
//
libros.results
|> Array.iter (fun libro -> printfn $"{libro.title}")

//
// en este mapa estan los formatos y deonde obtener
// el librp mismo. Ejemplo para el primer libro de la lista
//
printfn "----Formatos----"
libros.results[0].formats
|> Map.iter (fun key value -> printfn $"{key} -> {value}")