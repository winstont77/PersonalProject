<script>
import { onMounted, ref } from 'vue';
import { reactive } from 'vue';
import axios from 'axios'
import router from "../../router/router.js"

export default{
    setup() {
      let goto = (sport)=>{
        router.push({path:`/index/sport/${sport}`})
      }  
      return {goto}
    },
    components: {  }
}
</script>

<template>
    <div class="wc-PageView">
        <div class="wc-PageView_Main">
            <div class="wcl-CommonElementStyle_NavContentContainer">
                <div class="wcl-CommonElementStyle_WebNav">
                    <div class="wn-WebNavModule">
                        <div class="wn-Menu">
                            <div class="wn-PreMatchGroup">
                                <div class="wl-OfferItem">
                                    <div class="wnd-WebNavModuleDefault_LHSOfferIcon"></div>
                                    <span>優惠</span>
                                </div>
                                <div class="wn-PreMatchGroup_HeaderText " style="">完整列表</div>
                                <div class="wn-PreMatchItem ">
                                    <div class="wn-ClassificationIcon ">
                                        <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-1">
                                        </div>
                                    </div>
                                    <span>足球</span>
                                    <div class="wn-PreMatchItem_FavIcon"></div>
                                </div>
                                <div class="wn-PreMatchItem ">
                                    <div class="wn-ClassificationIcon ">
                                        <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-2">
                                        </div>
                                    </div>
                                    <span>美式足球</span>
                                    <div class="wn-PreMatchItem_FavIcon"></div>
                                </div>
                                <div class="wn-PreMatchItem ">
                                    <div class="wn-ClassificationIcon ">
                                        <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-3">
                                        </div>
                                    </div>
                                    <span>棒球</span>
                                    <div class="wn-PreMatchItem_FavIcon"></div>
                                </div>
                                <div class="wn-PreMatchItem " v-on:click="goto('basketball')">
                                    <div class="wn-ClassificationIcon ">
                                        <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-4">
                                        </div>
                                    </div>
                                    <span>籃球</span>
                                    <div class="wn-PreMatchItem_FavIcon"></div>
                                </div>
                                <div class="wn-PreMatchItem ">
                                    <div class="wn-ClassificationIcon ">
                                        <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-5">
                                        </div>
                                    </div>
                                    <span>乒乓球</span>
                                    <div class="wn-PreMatchItem_FavIcon"></div>
                                </div>
                                <div class="wn-PreMatchItem ">
                                    <div class="wn-ClassificationIcon ">
                                        <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-6">
                                        </div>
                                    </div>
                                    <span>冰上曲棍球</span>
                                    <div class="wn-PreMatchItem_FavIcon"></div>
                                </div>
                                <div class="wn-PreMatchItem ">
                                    <div class="wn-ClassificationIcon ">
                                        <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-7">
                                        </div>
                                    </div>
                                    <span>司諾克</span>
                                    <div class="wn-PreMatchItem_FavIcon"></div>
                                </div>
                                <div class="wn-PreMatchItem ">
                                    <div class="wn-ClassificationIcon ">
                                        <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-8">
                                        </div>
                                    </div>
                                    <span>排球</span>
                                    <div class="wn-PreMatchItem_FavIcon"></div>
                                </div>
                                <div class="wn-PreMatchItem ">
                                    <div class="wn-ClassificationIcon ">
                                        <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-9">
                                        </div>
                                    </div>
                                    <span>田徑</span>
                                    <div class="wn-PreMatchItem_FavIcon"></div>
                                </div>
                                <div class="wn-PreMatchItem ">
                                    <div class="wn-ClassificationIcon ">
                                        <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-10">
                                        </div>
                                    </div>
                                    <span>賽狗</span>
                                    <div class="wn-PreMatchItem_FavIcon"></div>
                                </div>
                                <div></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="wcl-PageContainer">
                    <!-- <div class="wcl-PageContainer_Colcontainer"></div> -->
                    <RouterView></RouterView>
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

.wcl-CommonElementStyle_WebNav {
    flex: 1 0 270px;
    max-width: 270px;
    background-color: #282828;
    height: 100%;
}

.wn-WebNavModule{
    font-size: 13px;
    height: 100%;
    color: #ddd;
    font-size: 14px;
    line-height: 25px;
    overflow: hidden;
}

.wn-Menu{
    overflow-y: auto;
    overflow-x: hidden;
    overscroll-behavior: none;
    height: 100%;
}

.wn-PreMatchGroup{
    position: relative;
}

.wl-OfferItem{
    color: #fff;
    background-color: #282828;
    font-weight: 700;
    cursor: default;
    display: flex;
    height: 45px;
    margin-top: 20px;
    padding-left: 20px;
    position: relative;
    line-height: 45px;
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
}

.wl-OfferItem:hover{
    background-color: #383838;
    color: #fff;
}

.wnd-WebNavModuleDefault_LHSOfferIcon{
    align-self: center;
    width: 15px;
    height: 15px;
    display: flex;
    margin-right: 10px;
    background-image: url(../../images/sideMenu/offer.svg);
    background-size: 100%;
    background-repeat: no-repeat;
}

.wn-PreMatchGroup_HeaderText{
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
    line-height: 13px;
    padding: 32px 10px 10px 20px;
    font-size: 11px;
    text-transform: uppercase;
    color: #00DEA9;
    font-weight: 700;
    background-color: #282828;
    padding: 32px 0 10px 20px;
    cursor: default;
}

.wn-PreMatchItem {
    display: flex;
    align-items: center;
    height: 45px;
    position: relative;
    line-height: 45px;
    cursor: pointer;
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
    padding-left: 20px;
}

.wn-PreMatchItem:hover{
    background-color: #383838;
    color: #fff;
}

.wn-ClassificationIcon {
    width: 15px;
    height: 15px;
    margin-right: 10px;
}

.cis-ClassificationIconSmall{
    background-position: 50%;
    background-repeat: no-repeat;
    background-size: contain;
    height: 18px;
    width: 18px;
}

.wn-PreMatchItem_FavIcon{
    display: block;
    height: 45px;
    width: 45px;
    position: absolute;
    top: 0;
    right: 0;
    cursor: pointer;
}

#cis-ClassificationIconSmall-1{
    background-image: url(../../images/sideMenu/soccer.svg);
}

#cis-ClassificationIconSmall-2{
    background-image: url(../../images/sideMenu/football.svg);
}

#cis-ClassificationIconSmall-3{
    background-image: url(../../images/sideMenu/baseball.svg);
}

#cis-ClassificationIconSmall-4{
    background-image: url(../../images/sideMenu/basketball.svg);
}

#cis-ClassificationIconSmall-5{
    background-image: url(../../images/sideMenu/tabletennis.svg);
}

#cis-ClassificationIconSmall-6{
    background-image: url(../../images/sideMenu/icehockey.svg);
}

#cis-ClassificationIconSmall-7{
    background-image: url(../../images/sideMenu/snooker.svg);
}

#cis-ClassificationIconSmall-8{
    background-image: url(../../images/sideMenu/volleyball.svg);
}

#cis-ClassificationIconSmall-9{
    background-image: url(../../images/sideMenu/athletics.svg);
}

#cis-ClassificationIconSmall-10{
    background-image: url(../../images/sideMenu/greyhoundracing.svg);
}

.wcl-PageContainer{
    display: flex;
    flex-direction: column;
    width: 100%;
    height: 100%;
    overflow-y: auto;
    overflow-x: hidden;
    overscroll-behavior: none;
}
</style>