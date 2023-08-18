<script>
import { onMounted } from 'vue';
import { reactive } from 'vue';
import router from "../router/router.js"

export default{
    setup(){

        let signupButton=()=>{
            router.push({path:"/signup"})
        }

        let signinButton=()=>{
            router.push({path:"/signin"})
        }
        
        onMounted(()=>{
            
        })
        return {signupButton, signinButton}
    }
}
</script>
<template>
    <div class="wc-WebConsoleModule_SiteContainer">
        <div class="wc-WebConsoleModule_Header">
            <div class="hm-HeaderModule">
                <div class="hm-ProductHeaderWide">
                    <div class="hm-ProductHeaderWide_Container">
                        <div class="hm-HeaderScroller ">
                            <div class="hm-HeaderScroller_LeftArrow"></div>
                            <div class="hm-HeaderScroller_HScroll ">
                                <div class="hm-ProductHeaderWide_ScrollContainer">
                                    <!-- <div class="hm-ProductHeaderWide_Link">
                                        <div class="hm-HeaderMenuItem_Link">體育投注</div>
                                    </div> -->
                                </div>
                            </div>
                            <div class="hm-HeaderScroller_RightArrow"></div>
                        </div>
                    </div>
                    <div class="hm-ProductHeaderWide_RHS">
                        <div class="hm-ProductHeaderWideResponsibleGambling ">有節制博彩</div>
                        <div class="hm-ProductHeaderWideHelp ">幫助</div>
                    </div>
                </div>
                <div class="hm-MainHeaderWide">
                    <div class="hm-MainHeaderLogoWide">
                        <div class="hm-MainHeaderLogoWide_Bet365LogoImage"></div>
                    </div>
                    <div class="hm-MainHeaderRHSLoggedOutWide">
                        <div class="hm-MainHeaderRHSLoggedOutWide_JoinContainer" v-on:click="signupButton">
                            <div>註冊</div>
                        </div>
                        <div class="hm-MainHeaderRHSLoggedOutWide_LoginContainer" v-on:click="signinButton">
                            <div>登錄</div>
                        </div>
                    </div>
                </div>
            </div>
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
                                        <div class="wn-PreMatchItem ">
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
        </div>
    </div>
</template>

<style>
.wc-WebConsoleModule_SiteContainer {
}

.wc-WebConsoleModule_Header{
    position: sticky;
    width: 100%;
    top: 0;
    z-index: 50;
}

.hm-HeaderModule{
    width: 100%;
    z-index: 100;
    min-width: 320px;
    position: relative;
    display: flex;
    flex-direction: column;
}

.hm-ProductHeaderWide{
    margin-right: 15px;
    display: flex;
    justify-content: space-between;
    background-color: #333;
    font-size: 12px;
    line-height: 13px;
    width: 100%;
    height: 45px;
    padding: 0 calc((100vw - 1450px)/2);
}

.hm-ProductHeaderWide_Container {
    white-space: nowrap;
    margin-left: 20px;
    display: flex;
    overflow: hidden;
    align-items: center;
    align-self: center;
    position: relative;
    flex: 1 1 100%;
    font-size: 12px;
    line-height: 13px;
}

.hm-HeaderScroller {
    padding: 0;
    margin: 0;
    width: 100%;
    position: relative;
}

.hm-HeaderScroller_LeftArrow{
    opacity: 0;
    visibility: hidden;
    transition: opacity .4s,visibility .4s;
    transition-delay: .2s;
    width: 20px;
    background-color: #333;
    height: 100%;
    background-size: 7px 12px;
    background-position: 50%;
    background-repeat: no-repeat;
    position: absolute;
    top: 0;
    z-index: 10;
    cursor: pointer;
    left: 0;
    border-right: 1px solid #545454;
}

.hm-HeaderScroller_HScroll {
    white-space: nowrap;
    display: block;
}

.hm-ProductHeaderWide_ScrollContainer{
    overflow-y: auto;
    height: 30px;
}

.hm-ProductHeaderWide_Link{
    display: inline-flex;
    align-items: center;
    height: 100%;
    font-size: 12px;
    margin-right: 20px;
    color: #fff;
    cursor: pointer;
    transition-property: opacity;
    transition-duration: .2s;
}

.hm-HeaderScroller_RightArrow{
    opacity: 0;
    visibility: hidden;
    transition: opacity .4s,visibility .4s;
    transition-delay: .2s;
    width: 20px;
    background-color: #333;
    height: 100%;
    background-size: 7px 12px;
    background-position: 50%;
    background-repeat: no-repeat;
    position: absolute;
    top: 0;
    z-index: 10;
    cursor: pointer;
    right: 0;
    border-left: 1px solid #545454;
}

.hm-ProductHeaderWide_RHS{
    height: 100%;
    margin-right: 20px;
    display: flex;
    flex: 1 0 auto;
}

.hm-ProductHeaderWideResponsibleGambling {
    font-size: 12px;
    display: inline-flex;
    align-items: center;
    height: 100%;
    margin-right: 20px;
    color: #9c9c9c;
    cursor: pointer;
    flex-shrink: 0;
}

.hm-ProductHeaderWideResponsibleGambling:hover{
    color: #fff;
}

.hm-ProductHeaderWideHelp {
    font-size: 11px;
    display: inline-flex;
    align-items: center;
    height: 100%;
    margin-right: 20px;
    color: #9c9c9c;
    cursor: pointer;
    flex-shrink: 0;
    margin-right: 0;
}

.hm-ProductHeaderWideHelp:hover{
    color: #fff;
}

.hm-MainHeaderWide{
    z-index: 20;
    display: flex;
    align-items: center;
    position: relative;
    background-color: #126e51;
    height: 60px;
    justify-content: space-between;
    padding: 0 calc((100vw - 1450px)/2);
}

.hm-MainHeaderLogoWide{
    display: flex;
    height: 100%;
    flex: 0 1 25%;
}

.hm-MainHeaderLogoWide_Bet365LogoImage {
    background-repeat: no-repeat;
    background-position: 50%;
    height: 100%;
    position: relative;
    margin-left: 20px;
    width: 100px;
    background-size: contain;
    background-image: url(../images//logos/indexLogo.svg);
    cursor: pointer;
}

.hm-MainHeaderRHSLoggedOutWide {
    display: flex;
    justify-content: flex-end;
    align-items: center;
    flex: 1 0 25%;
    height: 100%;
    white-space: nowrap;
    transition-property: opacity;
    transition-duration: .2s;
}

.hm-MainHeaderRHSLoggedOutWide_JoinContainer{
    border-radius: 2px;
    padding: 0 10px;
    height: 26px;
    color: #333;
    background-color: #ffdf1b;
    display: flex;
    align-items: center;
    margin-left: 20px;
    font-size: 13px;
    line-height: 17px;
    flex: 0 0 auto;
    cursor: pointer;
}

.hm-MainHeaderRHSLoggedOutWide_JoinContainer:hover{
    background-color: #e4e4e4;
}

.hm-MainHeaderRHSLoggedOutWide_LoginContainer{
    display: flex;
    align-items: center;
    color: #fff;
    margin: 0 20px 0 15px;
    font-size: 13px;
    line-height: 17px;
    height: 100%;
    cursor: pointer;
}

.hm-MainHeaderRHSLoggedOutWide_LoginContainer:hover{
    color: #26ffbe;
}

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
    background-image: url(../images/sideMenu/offer.svg);
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
    background-image: url(../images/sideMenu/soccer.svg);
}

#cis-ClassificationIconSmall-2{
    background-image: url(../images/sideMenu/football.svg);
}

#cis-ClassificationIconSmall-3{
    background-image: url(../images/sideMenu/baseball.svg);
}

#cis-ClassificationIconSmall-4{
    background-image: url(../images/sideMenu/basketball.svg);
}

#cis-ClassificationIconSmall-5{
    background-image: url(../images/sideMenu/tabletennis.svg);
}

#cis-ClassificationIconSmall-6{
    background-image: url(../images/sideMenu/icehockey.svg);
}

#cis-ClassificationIconSmall-7{
    background-image: url(../images/sideMenu/snooker.svg);
}

#cis-ClassificationIconSmall-8{
    background-image: url(../images/sideMenu/volleyball.svg);
}

#cis-ClassificationIconSmall-9{
    background-image: url(../images/sideMenu/athletics.svg);
}

#cis-ClassificationIconSmall-10{
    background-image: url(../images/sideMenu/greyhoundracing.svg);
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