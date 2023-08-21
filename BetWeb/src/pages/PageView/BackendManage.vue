<script>
import { onMounted, ref } from 'vue';
import { reactive } from 'vue';
import axios from 'axios'
import router from "../../router/router.js"

export default{
    setup() {
        onMounted(()=>{
            
        })
    },
    components: {  }
}
</script>

<template>
    <div class="wc-PageView">
        <div class="wc-PageView_Main">
            <div class="wcl-CommonElementStyle_NavContentContainer">
                <div class="myb-MyBetsModule">
                    <div class="myb-MyBets">
                        <div class="myb-MyBetsHeader">
                            <div class="myb-MyBetsHeader_Scroller">
                                <div class="myb-HeaderButton">未結算</div>
                                <div class="myb-HeaderButton">已結算</div>
                                <div class="myb-HeaderButton">全部</div>
                            </div>
                        </div>
                        <div class="myb-MyBetsScroller">
                            <div>
                                <div class="myb-MyBets_Container">
                                    <div class="myb-BetItemsContainer_EmptyMessage">
                                        <div class="myb-BetItemsContainer_NoBetsMessageLineOne">
                                            當前無投注可顯示
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style>
.wc-PageView{
    display: flex;
    align-items: stretch;
    margin: 0 auto;
    max-width: 1450px;
    height: calc(100vh - 105px);
}

.wc-PageView_Main{
    flex: 1 1 auto;
    width: auto;
    position: relative;
    display: flex;
    align-items: stretch;
    min-width: 0;
    height: 100%;
    background-color: #383838;
}

.wcl-CommonElementStyle_NavContentContainer {
    display: flex;
    align-items: stretch;
    width: 100%;
    height: 100%;
}

.myb-MyBetsModule{
    width: 100%;
    height: 100%;
    background-color: #282828;
}

.myb-MyBets{
    position: relative;
    display: flex;
    flex-direction: column;
    height: 100%;
}

.myb-MyBetsHeader{
    position: relative;
    width: 100%;
    display: flex;
    height: 50px;
    background-image: none;
    font-size: 0;
    z-index: 1;
}

.myb-MyBetsHeader_Scroller{
    overflow-x: hidden;
    margin: 0 20px;
    overflow-y: hidden;
    height: 50px;
    flex: 1 1 100%;
    white-space: nowrap;
    padding: 0;
    width: 100%;
    position: relative;
}

.myb-HeaderButton{
    color: #ddd;
    font-weight: 700;
    line-height: 50px;
    height: 50px;
    padding: 0 10px;
    display: inline-flex;
    align-items: center;
    cursor: pointer;
    flex-direction: column;
    position: relative;
    z-index: 1;
    min-width: 40px;
    font-size: 13px;
}

.myb-MyBetsScroller{
    overflow-y: auto;
    overflow-x: hidden;
    overscroll-behavior: none;
    height: calc(100% - 50px);
}

.yb-MyBets_Container{
    min-height: calc(100vh - 150px);
    margin: 0 30px 0 20px;
    backface-visibility: hidden;
    position: relative;
    transition: transform .5s ease;
}

.myb-BetItemsContainer_EmptyMessage {
    padding: 30px 15px;
    background: #353535;
    color: #ddd;
    text-align: center;
    position: relative;
    border-radius: 5px;
    margin: 0 20px;
}

.myb-BetItemsContainer_NoBetsMessageLineOne{
    font-weight: 700;
    font-size: 14px;
    margin-bottom: 10px;
    line-height: 18px;
}
</style>