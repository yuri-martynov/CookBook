module Recipe

let get (dish: string) : Async<string> = async {
    return "Мы научим Вас готовить " + dish
}
