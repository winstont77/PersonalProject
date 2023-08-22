<script>
import { onMounted, ref } from 'vue';
import { reactive } from 'vue';
import axios from 'axios'
import router from "../router/router"

export default{
    setup() {
        
    },
    components: {  }
}
</script>

<template>
   <div class="wrapper">
        <div class="sidebar">
            <div class="sidebar-wrapper">
                <div class="logo">
                    <a href="" class="simple-text">
                        <div class="logo-img">
                            <img src="../images/backendManage/91applogo.png" style="width: 40px;height: 40px;margin: auto;" alt="">
                        </div>
                        DashBoard
                    </a>
                </div>
                <ul class="nav">
                    <div class="nav-item">
                        <a href="" class="nav-link">
                            <p>Dashboard</p>
                        </a>
                    </div>
                    <div></div>
                    <div></div>
                </ul>
            </div>
        </div>
        <div class="main-panel"></div>
   </div>
</template>

<style>
.wrapper{
    position: relative;
    top: 0;
    height: 100vh;
    min-height: 100vh;
}

.sidebar{
    overflow: auto;
    max-height: 100%;
    height: 100%;
    transition-property: top,bottom;
    transition-duration: .2s,.2s;
    transition-timing-function: linear,linear;
    background-image: url(../images/backendManage/sidebar-4.jpg);
    position: absolute;
    top: 0;
    bottom: 0;
    left: 0;
    width: 260px;
    display: block;
    z-index: 1;
    color: #fff;
    font-weight: 200;
    background-size: cover;
    background-position: 50%;
}

.sidebar::before{
    opacity: .33;
    background: #000;
    display: block;
    content: "";
    position: absolute;
    width: 100%;
    height: 100%;
    top: 0;
    left: 0;
    z-index: 2;
    color: #fff;
    font-weight: 200;
}

.sidebar-wrapper{
    position: relative;
    max-height: calc(100vh - 75px);
    min-height: 100%;
    overflow: auto;
    width: 260px;
    z-index: 4;
    padding-bottom: 100px;
    color: #fff;
    font-weight: 200;
}

.sidebar-wrapper:after{
    background: linear-gradient(180deg,#35495e,#40b782);
    background-size: 150% 150%;
    opacity: .77;
    z-index: 3;
    display: block;
    content: "";
    position: absolute;
    width: 100%;
    height: 100%;
    top: 0;
    left: 0;
}

.logo{
    padding: 10px 5px;
    margin: 0 20px;
    border-bottom: 1px solid hsla(0,0%,100%,.2);
    position: relative;
    z-index: 4;
}

.simple-text{
    text-transform: uppercase;
    padding: 0;
    display: flex;
    font-size: 18px;
    color: #fff;
    text-align: left;
    font-weight: 400;
    line-height: 40px;
    text-decoration: none;
}

.logo-img{
    width: 40px;
    display: flex;
    height: 40px;
    margin-left: 0;
    margin-right: 10px;
    background: #fff;
    border-radius: 40px;
    text-align: center;
}

.nav{
    margin-top: 20px;
    float: none;
    display: block;
    padding-left: 0;
    margin-bottom: 0;
    list-style: none;
}

.nav-item{
}

.nav-link{
    color: #fff;
    margin: 5px 15px;
    opacity: .86;
    border-radius: 4px;
    display: block;
    padding: 10px 15px;
}
</style>