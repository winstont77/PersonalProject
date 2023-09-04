import { createApp } from 'vue'
import './style.css'
import router from './router/router.js'
import App from './App.vue'

createApp(App).use(router).mount('#app')

if(
    localStorage.getItem("username")==undefined||
    localStorage.getItem("username")==null||
    localStorage.getItem("token")==undefined||
    localStorage.getItem("token")==null){
        router.push({path:"/signout"})
}