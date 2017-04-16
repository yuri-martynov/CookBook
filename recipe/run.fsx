#r "System.Net.Http"
#r "Newtonsoft.Json"
#r "System.Runtime.Serialization"
#r "CookBook"

//#if !COMPILED
//#I "../../bin/Binaries/WebJobs.Script.Host"
//#r "Microsoft.Azure.WebJobs.Host.dll"
//#endif

open System.Net
open System.Net.Http
open Newtonsoft.Json
open System.Runtime.Serialization
open System
//open Microsoft.Azure.WebJobs.Host

type Parameters = {
    dish: string;
    product: string
}

type Result = {
    action: string;
    parameters: Parameters
}

type RecipeRequest = {
    result: Result
}

[<DataContract>]
type RecipeResponse = {
    [<field: DataMember(Name="speech")>]
    speech: string;
    [<field: DataMember(Name="displayText")>]
    displayText: string;
}


let answer (result: Result) : Async<string> =
    let dish = result.parameters.dish
    match result.action with
    | "recipe" -> Recipe.get dish
    | "ingradients" -> Ingradients.get dish
    | "contains" -> Contains.get dish result.parameters.product
    | "time" ->  Time.get dish // ????

let Run(req: HttpRequestMessage, log: TraceWriter) =
    async {

        let folder = Environment.ExpandEnvironmentVariables(@"%HOME%")
        let files = System.IO.Directory.EnumerateFiles(folder)
        return req.CreateResponse(HttpStatusCode.OK, files)

        log.Info("Webhook was triggered!")
        let! jsonContent = req.Content.ReadAsStringAsync() |> Async.AwaitTask

        try
            let recipe = JsonConvert.DeserializeObject<RecipeRequest>(jsonContent)
            let! answer' =  answer recipe.result
            return req.CreateResponse(HttpStatusCode.OK, 
                { displayText = "From webhook"; speech = answer' })
        with _ ->
            return req.CreateResponse(HttpStatusCode.BadRequest)

    } |> Async.StartAsTask


