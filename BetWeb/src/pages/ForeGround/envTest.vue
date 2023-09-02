<script>
import {onMounted, ref} from 'vue'
import axios from "axios"
export default{
    setup(){
        onMounted(()=>{
            console.log(import.meta.env.VITE_API_URL)

            axios.get(import.meta.env.VITE_API_URL +　"/GetTestENV",{})
            .then(res=>{
                console.log(res)
            })
            .catch(err=>{
                console.log(err)
            })
        })
    }
}
</script>

<template>
    envTest
</template>

<style scoped>

</style>