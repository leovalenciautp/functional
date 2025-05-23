open System
open System.Threading


//
// Cada bala va a llevar su propio estado
//
type BulletState={
    X: int
    Y: int
    Fired: bool
}

//
// Variable de estado de nuestro programa
// Aqui deben ir las cosas que cambian con el
// paso del tiempo, o segun el input del usuario
//
type State = {
    Tick: int
    Timer: int
    Continuar: bool
    MoonTheta: float
    AlienX: int
    AlienY: int
    BulletList: BulletState list //Las balas activas son una lista ahora
}

//
// Tiempo en milisegundos que el eventLoop
// va a dormir.
//
let eventClock = 20

//
// Numero de vueltas en el eventLoop
// para que transcurra un segundo
//
let oneSecond = 1000/eventClock

//
// Almacenamos el ancho y alto de la pantalla 
// en estas dos variables
//
let screenWidth = Console.BufferWidth
let screenHeight = Console.BufferHeight

//
// Calculamos el centro de la pantalla
// recuerden restar uno porque las coordenadas
// empiezan en 0
//
let centerX = screenWidth/2-1
let centerY = screenHeight/2-1

//
// Periodo orbital de la "luna"
// En este caso da uaa vuelta completa cada
// cinco segundos
//
let moonPeriod = 5.0

//
// Definir la velocidad angular para que una vuelta 
// entera que son 2*PI radianes, ocurra en el periodo
// deseado.
//
let moonAngularSpeed = 2.0*Math.PI/(float oneSecond*moonPeriod) 

//
// Valor inicial de nuestra variable de estado
//
let initialState = {
    Tick=0
    Timer=0
    Continuar=true
    MoonTheta=0.0
    AlienX = centerX
    AlienY = centerY
    BulletList = [] 
}

//
// En esta aplicacion el paso del tiempo esta marcado
// por cada Tick que pasa. Esta función actualiza
// el estado incrementando el número de ticks en cada
// vuelta del eventLoop
//
let updateTick state =
    {state with Tick=state.Tick+1}

//
// Cuando una bala aparece en la pantalla, inicializamos
// el estado y la agregamos a la lista actual de balas
//
let createNewBullet bulletList state =
    let bullet = {X=state.AlienX+2;Y=state.AlienY;Fired=true}
    bullet :: bulletList

//
// Keyboard function para activar una bala, lo hacemos
// con la barra espaciadora
//
let updateBulletKeyboard key state =
    match key with
    | ConsoleKey.Spacebar ->
        let bulletList = createNewBullet state.BulletList state
        {state with BulletList=bulletList}
    | _ -> state

//
// Keyboard function para controlar al Alien que aparece en la 
// pantalla. Lo podemos mover con las flechas.
// Noten que tenemos codigo para evitar que el Alien se salga
// de la pantalla (no se peermite coordenadas negativas, ni
// mayores al ancho y alto de la pantalla)
//
let updateAlienKeyboard key state =
    let newX, newY =
        match key with
        | ConsoleKey.UpArrow ->
            state.AlienX, max 0 (state.AlienY-1)
        | ConsoleKey.DownArrow ->
            state.AlienX, min (screenHeight-1) (state.AlienY+1)
        | ConsoleKey.RightArrow ->
            min (screenWidth-2) (state.AlienX+1), state.AlienY
        | ConsoleKey.LeftArrow ->
            max 0 (state.AlienX-1), state.AlienY
        | _ -> state.AlienX, state.AlienY

    {state with AlienX=newX;AlienY=newY}

//
// Funcion principal de manejo de teclado, aqui llamamos
// a la funcion de teclado de cada componente que lo necesite
//
let updateKeyboard key state =
    state
    |> updateAlienKeyboard key
    |> updateBulletKeyboard key
//
// Importante chequear por una condicion de salida
// en esta App, presionando la tecla Escape hace
// que el eventLoop pare. Si cualquier otra tecla es presionada
// llamamos a la funcion que maneja el teclado.
//
let updateExitState state =
    //
    // Es important chequear si esta disponible una tecla
    // para que la aplicacion no se bloquee en ReadKey()
    //
    if Console.KeyAvailable then
        match Console.ReadKey(true).Key with
        | ConsoleKey.Escape ->
            {state with Continuar=false}
        | key -> updateKeyboard key state
    else
        state
//
// El timer de la esquina lleva cuanta de los segundos.
// Usamos la operacion remainder para ver si si cumplieron
// los ciclos por cada segundo
//
let updateTimer state =
    if state.Tick % oneSecond = 0 then
        {state with Timer=state.Timer+1}
    else
        state

//
// La orbita del satelite es muy simple, el angugulo es
// 𝛳 = ω*t, donde ω es la velocidad angular del satelite
//
let updateMoon state =
    {state with MoonTheta = (float state.Tick)*moonAngularSpeed}

//
// En cada ciclo del eventLoop tenemos que animar las balas
// La idea es recorrer la lista de balas y actualizar su
// coordenada X. Si la bala se sale de la margen derecha de la
// pantalla, se borra de la lista.
// 

let updateBullet state =
    
    let newBulletList =
        state.BulletList
        |> Seq.map (fun bullet ->
            let newX = bullet.X+1
            let fired = not (newX > screenWidth-2)
            {bullet with X=newX;Fired=fired}
        )
        |> Seq.filter (fun bullet -> bullet.Fired)
        |> Seq.toList

    {state with BulletList = newBulletList}
    
// Función principal que actualiza el state del app
// Noten como usamos un pipeline para eso
//
let updateState state =
    state
    |> updateTick
    |> updateExitState
    |> updateTimer
    |> updateMoon
    |> updateBullet


//
// Funcion generica para escribir algo en la pantalla
// en las coordenads cartesianas x,y con el color deseado
//
let displayMessagetAt x y color (message:string) =

    //
    // Guardamos el estado actual del color y la posicion del 
    // cursor
    let oldColor = Console.ForegroundColor
    let curX = Console.CursorLeft
    let curY = Console.CursorTop

    //
    // Aqui cambiamos el color, y la posicion
    // y se escribe el mensaje
    //
    Console.ForegroundColor <- color
    Console.SetCursorPosition(x,y)
    Console.Write message

    //
    // Finalmente restauramos todo como estaba
    //
    Console.ForegroundColor <- oldColor
    Console.SetCursorPosition(curX,curY)


//
// Esta version es la misma que la anterior pero usando 
// coordenadas polares
//
let displayMessagetAtPolar r theta color (message:string) =
  
    //
    // Esta es la conversion standard entre polar y cartesiano
    //
    //  x = r*cos(𝛳)
    //  y = r*sin(𝛳)
    //
    // Usamos un factor de 2.0 en x para compenzar el aspect ratio
    //
    let posX = 2.0*r*Math.Cos theta
    let posY = r*Math.Sin theta

    //
    // Aqui hacemos una translacion para que la orbita quede
    // centrada en la pantalla
    //
    let x = centerX + (int posX)
    let y = centerY - (int posY)

    displayMessagetAt x y color message


//
// El timer de segundos lo ponemos en la esquina
// superior derecha
//
let displayTimer state =
    let msg = $"Reloj: {state.Timer}"
    let x = screenWidth-msg.Length
    displayMessagetAt x 0 ConsoleColor.Cyan msg

//
// El satelite orbita con un radio de 8 por ahora
//
let displayMoon state =
    displayMessagetAtPolar 8.0 state.MoonTheta ConsoleColor.Yellow "👾" 

//
// Funcion para borrar la imagen pasada para que la animacion
// se vea bien
//
let clearMoon state =
    displayMessagetAtPolar 8.0 state.MoonTheta ConsoleColor.Yellow "  "

//
// Funcion para mostrar el Alien
//
let displayAlien state =
    displayMessagetAt state.AlienX state.AlienY ConsoleColor.Yellow "👽"

//
// Funcion para borrar el alien.
//

let clearAlien state =
    displayMessagetAt state.AlienX state.AlienY ConsoleColor.Yellow "  "

//
// Para mostrar las balas, tenemos que iterar la lista y mostrar
// una por una
//
let displayBullet state =
    state.BulletList
    |> Seq.iter (fun bullet ->
        displayMessagetAt bullet.X bullet.Y ConsoleColor.Red "=>"
    )

//
// Lo mismo hacemos para borrar las balas, se itera la lista
// y se borran individualmente
//
let clearBullet state =
    state.BulletList
    |> Seq.iter (fun bullet ->
        displayMessagetAt bullet.X bullet.Y ConsoleColor.Red "  "
    )
// 
// Funcion principal para visualizar
// cada objecto en la pantalla
//
let displayApp state =
    displayTimer state
    displayMoon state
    displayAlien state
    displayBullet state

//
// Funcion principal para borrar objectos que sean
// animados
//
let clearApp state =
    clearMoon state
    clearAlien state   
    clearBullet state
//
// Es importante que el eventLoop duerma unos cuantos
// milisegundos para no ocupar el CPU todo el tiempo
//
let performSleep() =
    Thread.Sleep eventClock


//
// Definicion recursiva del mismo eventLoop
// En esta forma es mucho mas compacto.
//
let rec eventLoop state =
    if state.Continuar then
        clearApp state
        let newState = updateState state
        displayApp newState
        performSleep()
        eventLoop newState


//
// Es bueno ocultar el cursor en esta App
//
Console.CursorVisible <- false
Console.Clear()
//
// Real EventLoop for the application
//
// Seq.initInfinite (fun i -> i)
// |> Seq.scan ( fun state _ -> 
//     clearApp state
//     updateState state
// ) initialState 
// |> Seq.takeWhile ( fun state -> state.Continuar)
// |> Seq.iter ( fun state -> 
//     displayApp state
//     performSleep()
// )

eventLoop initialState

//
// Restaurando todo antes de retornar al
// sistema operativo
//
Console.Clear()
Console.CursorVisible <- true


