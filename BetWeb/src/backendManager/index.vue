<script>
import { onMounted, ref } from 'vue';
import { reactive } from 'vue';
import axios from 'axios'
import router from "../router/router"

export default{
    setup() {
        let goto=()=>{
            router.push({path:"index/tablelist"})
        }
        return {goto}
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
                            <img src="../images/backendManage/vuelogo.png" style="width: 21px;height: 21px;margin: auto;" alt="">
                        </div>
                        DashBoard
                    </a>
                </div>
                <ul class="nav">
                    <div class="nav-item">
                        <div href="" class="nav-link">
                            <p>Dashboard</p>
                        </div>
                    </div>
                    <div v-on:click="goto()">
                        <div href="" class="nav-link">
                            <p>Tablelist</p>
                        </div>
                    </div>
                    <div></div>
                </ul>
            </div>
        </div>
        <div class="main-panel">
            <div class="navbar">
                <div class="container-fluid">
                    <a href="" class="navbar-brand">Dashboard</a>
                    <div class="navbar-collapse">
                        <ul class="mr-auto"></ul>
                        <ul class="ml-auto">
                            <li class="nav-item">
                                <a class="nav-link" href="">Account</a>
                            </li>
                            <li class="dropdown-nav-item">
                                <a href="" class="nav-link-dropdown-toggle">
                                    <span>Dropdown</span>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a href="" class="nav-link" style="color: #888;">Log out</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <RouterView></RouterView>
        </div>
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
    position: absolute;
    z-index: 5;
    width: 100%;
    text-decoration: none;
    text-transform: uppercase;
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
    text-decoration: none;
    width: calc(100% - 30px);
}

.nav-link:hover{
    background: hsla(0,0%,100%,.13);
    opacity: 1;
    width: calc(100% - 30px);
}

.main-panel{
    overflow: auto;
    max-height: 100%;
    height: 100%;
    transition-property: top,bottom;
    transition-duration: .2s,.2s;
    transition-timing-function: linear,linear;  
    background: hsla(240,7%,81%,.15);
    position: relative;
    float: right;
    width: calc(100% - 260px);
    min-height: 100%;
}

.navbar{
    margin-bottom: 0;
    border: 0;
    font-size: 16px;
    border-radius: 0;
    min-height: 50px;
    background-color: hsla(0,0%,100%,.96);
    border-bottom: 1px solid rgba(0,0,0,.1);
    padding: 5px 15px;
}

.container-fluid{
    flex-wrap: nowrap;
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: 100%;
    padding-right: 15px;
    padding-left: 15px;
    margin-right: auto;
    margin-left: auto;
    font-size: 16px;
}

.navbar-brand{
    font-weight: 400;
    margin: 5px 0;
    font-size: 20px;
    color: #888;
    display: inline-block;
    padding-top: 0.3125rem;
    padding-bottom: 0.3125rem;
    line-height: inherit;
    white-space: nowrap;
    text-decoration: none;
}

.navbar-collapse{
    display: flex!important;
    flex-basis: auto;
    justify-content: flex-end!important;
    flex-grow: 1;
    align-items: center;
    font-size: 16px;
}

.mr-auto{
    align-items: center;
    flex-direction: row;
    margin-right: auto!important;
    display: flex;
    padding-left: 0;
    margin-bottom: 0;
    list-style: none;
    flex-wrap: wrap;
    margin-top: 0;
}

.ml-auto{
    align-items: center;
    flex-direction: row;
    margin-left: auto!important;
    display: flex;
    padding-left: 0;
    margin-bottom: 0;
    list-style: none;
    margin-top: 0;
}

.nav-item{

}

.nav-link{
    /* color: #888; */
    padding: 10px 15px;
    position: relative;
    display: inline-flex;
    line-height: 1.2;
}

.dropdown-nav-item{
    position: relative;
}

.nav-link-dropdown-toggle{
    color: #888;
    padding: 10px 15px;
    position: relative;
    display: inline-flex;
    line-height: 1.2;
    cursor: pointer;
    text-decoration: none;
}

.bmcontent{
    padding: 30px 15px;
    min-height: calc(100% - 123px);
}

.bmcontainer-fluid{
    width: 100%;
    padding-right: 15px;
    padding-left: 15px;
    margin-right: auto;
    margin-left: auto;
}
</style>