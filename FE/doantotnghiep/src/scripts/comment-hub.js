import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'

export default {
    install(Vue) {
        const connection = new HubConnectionBuilder()
            .withUrl(`https://localhost:44361/commentHub`)
            .configureLogging(LogLevel.Information)
            .build()
        // use new Vue instance as an event bus
        const productHub = new Vue()

        // every component will use this.$questionHub to access the event bus
        Vue.prototype.$productHub = productHub

        // Forward server side SignalR events through $questionHub, where components will listen to them


        connection.on('NewCommentAddedToProduct', (comment) => {
            debugger
            productHub.$emit('comment-added-to-product', comment)
        })
        connection.on('NewCommentAddedToAdmin', (comment) => {
            debugger
            productHub.$emit('comment-added-to-admin', comment)
        })

        let startedPromise = null

        function start() {
            startedPromise = connection.start().catch(err => {
                console.error('Failed to connect with hub', err)
                return new Promise((resolve, reject) => setTimeout(() => start().then(resolve).catch(reject), 5000))
            })
            return startedPromise
        }
        connection.onclose(() => start())

        start()
    }
}