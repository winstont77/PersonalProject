<script>
import {onMounted, ref} from 'vue'
import axios from "axios"
export default{
    setup(){
        let username = ref("")
        let password = ref(null)
        let signin = ()=>{
            console.log(username.value, password.value)
        }
        onMounted(()=>{
        })
        return {
            username,
            password,
            signin
        }
    }
}
</script>

<template>
    <div>
        <div class="signup">
            <div class="signup-element">
                <div class="signup-element-header-background">
                    <div class="signup-element-header-background-logo"></div>
                </div>
                <div class="signup-element-content">
                    <div class="signup-element-content-container">
                        <div class="signup-element-content-container-title">
                            <div class="signup-element-content-container-title-container">
                                <div class="signup-element-content-container-title-container-text">登入帳戶</div>
                            </div>
                        </div>
                        <div>
                            <div class="oam-OpenAccountModule">
                                <div class="oam-OpenAccountModule_Form">
                                    <div class="oam-FieldGroupWithBorderTopText">
                                        <div class="oam-FieldGroupWithBorderTopText_Header">登陸</div>
                                        <div class="oam-FieldInputNewUsername">
                                            <div class="oam-FieldInputNewUsername_InputRow" id="oam-FieldInputNewUsername_InputRow" v-on:click="focusName" >
                                                <input type="text" class="oam-FieldInputNewUsername_Input" placeholder="用戶名" v-model="username">
                                                <div class="oam-FieldInputNewUsername_InfoAnchor">
                                                    
                                                </div>
                                            </div>
                                        </div>
                                        <div class="oam-FieldInputNewPassword">
                                            <div class="oam-FieldInputNewPassword_InputRow" id="oam-FieldInputNewPassword_InputRow">
                                                <input type="password" class="oam-FieldInputNewPassword_Input" id="oam-FieldInputNewPassword_Input" placeholder="密碼" v-model="password">
                                                <span class="oam-FieldInputNewPassword_MaskToggle">顯示</span>
                                                <div class="oam-FieldInputNewPassword_InfoAnchor">
                                                
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div class="oam-FieldInputCheckboxTerms ">
                                            
                                        </div>
                                    </div>
                                    <div>
                                        <div class="signinerrorprompt" >使用者名稱或密碼輸入錯誤</div>
                                        <button class="oam-OAFieldSubmitButton" v-on:click="signin">加入</button>
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

<style scoped>
.signup{

}

.signup-element{
    display: flex;
    flex-direction: column;
}

.signup-element-header-background{
    align-items: center;
    position: relative;
    display: flex;
    height: 50px;
    background-color: #FF6B0F;
    justify-content: space-between;
}

.signup-element-header-background-logo{
    margin-left: 20px;
    height: 22px;
    width: 100px;
    flex-shrink: 1;
    flex-grow: 0;
    background-image: url(../images/logos/signupLogo.svg);
    background-repeat: no-repeat;
    background-size: contain;
    background-position: 50%;
    cursor: pointer;
}

.signup-element-content{
    justify-content: center;
    position: relative;
    flex: 1 0 auto;
    max-width: 1080px;
    margin: 0 auto;
    width: 100%;
    display: flex;
}

.signup-element-content-container{
    padding-bottom: 70px;
    flex: 0 1 530px;
    width: 100%;
}

.signup-element-content-container-title-container{
    cursor: default;
    height: auto;
    padding: 35px 15px 25px;
    display: flex;
    font-size: 22px;
    text-align: left;
    color: #282828;
    line-height: 32px;
    font-weight: 700;
    justify-content: space-between;
    min-height: 70px;
    position: relative;
}

.signup-element-content-container-title-container-text{
    padding-right: 0;
}

.oam-OpenAccountModule {
    width: 100%;
    padding: 0 15px;
}

.oam-FieldGroupWithBorderTopText {

}

.oam-FieldGroupWithBorderTopText_Header {
    font-size: 15px;
    font-weight: 700;
    color: #282828;
    line-height: 21px;
    margin-bottom: 15px;
    position: relative;
}

.oam-FieldInputNewUsername {
    display: flex;
    position: relative;
    flex-flow: column;
    margin: 0 0 15px;
}

.oam-FieldInputNewUsername_InputRow {
    flex-flow: row;
    background: #fff;
    border-radius: 2px;
    box-shadow: 0 1px 0 0 #d4d4d4;
    position: relative;
    display: flex;
}

.oam-FieldInputNewUsername_Input{
    flex: 1;
    height: 50px;
    padding-left: 15px;
    border: 0;
    color: #282828;
    background-color: #fff;
    font-size: 16px;
    font-weight: 700;
    line-height: 23px;
    border-radius: 2px 0 0 2px;
    overflow: visible;
}

.oam-FieldInputNewUsername_InfoAnchor {

}

.oam-FieldInputNewUsername_Tooltip{
    font-size: 12px;
    color: #282828;
    line-height: 18px;
    background: #f5f7f7;
    border: 1px solid #b5b5b5;
    border-radius: 2px;
    z-index: 110;
    margin: 0 10px;
    position: absolute;
    right: 0;
    top: 48px;
    overflow-wrap: break-word;
    padding: 15px;
    margin-right: 0;
    display: none;
}

.oam-FieldInputNewUsername_Tooltip::before{
    right: 14px;
    content: "";
    position: absolute;
    bottom: 100%;
    width: 0;
    height: 0;
    border: 7px solid transparent;
    border-bottom: 7px solid #b5b5b5;
}

.oam-FieldInputNewUsername_TooltipText{
    font-size: 12px;
    color: #282828;
    line-height: 18px;
    overflow-wrap: break-word;
}

.oam-FieldInputNewUsername_InfoButton {
    display: block;
    cursor: pointer;
    height: 50px;
    width: 45px;
    border: 0;
    padding: 0;
    padding-left: 15px;
    border-radius: 0 2px 2px 0;
    background-color: #fff;
    background-image: url(../images/exclamation.svg);
    background-repeat: no-repeat;
    background-position: 50%;
}

.oam-FieldInputNewUsername_RightText{
    right: 30px;
    font-size: 12px;
    color: #757575;
    padding: 0 15px;
    text-align: right;
    background-color: transparent;
    position: absolute;
    top: 50%;
    transform: translateY(-50%);
    border: 0;
    transition: opacity .15s ease 0ms;
    opacity: 1;
}

.oam-FieldInputNewPassword {
    display: flex;
    flex-flow: column;
    position: relative;
}

.oam-FieldInputNewPassword_InputRow {
    position: relative;
    display: flex;
    flex-flow: row;
    background: #fff;
    border-radius: 2px;
    box-shadow: 0 1px 0 0 #d4d4d4;
    margin-bottom: 15px;
}

.oam-FieldInputNewPassword_Input{
    flex: 1;
    height: 50px;
    padding-left: 15px;
    border: 0;
    color: #282828;
    background-color: #fff;
    font-size: 16px;
    font-weight: 700;
    line-height: 23px;
    border-radius: 2px 0 0 2px;
    overflow: visible;
}

.oam-FieldInputNewPassword_MaskToggle {
    height: 50px;
    border: 0;
    padding: 0 10px 0 15px;
    background-color: #fff;
    font-weight: 700;
    font-size: 12px;
    cursor: pointer;
    border-radius: 0;
    display: flex;
    align-items: center;
    color: #FF6B0F;
}

.oam-FieldInputNewPassword_InfoAnchor {

}

.oam-FieldInputNewPassword_Tooltip{
    margin-right: 0;
    font-size: 12px;
    color: #282828;
    line-height: 18px;
    background: #f5f7f7;
    border: 1px solid #b5b5b5;
    border-radius: 2px;
    z-index: 110;
    margin: 0 10px;
    position: absolute;
    right: 0;
    top: 48px;
    overflow-wrap: break-word;
    padding: 15px;
    display: none;
}

.oam-FieldInputNewPassword_Tooltip::before{
    right: 14px;
    content: "";
    position: absolute;
    bottom: 100%;
    width: 0;
    height: 0;
    border: 7px solid transparent;
    border-bottom: 7px solid #b5b5b5;
}

.oam-FieldInputNewPassword_Tooltip::after{
    right: 15px;
    border: 6px solid transparent;
    border-bottom: 6px solid #f5f7f7;
    content: "";
    position: absolute;
    bottom: 100%;
    width: 0;
    height: 0;
}

.oam-FieldInputNewPassword_TooltipText{
    font-size: 12px;
    color: #282828;
    line-height: 18px;
}

.oam-FieldInputNewPassword_InfoButton {
    display: block;
    cursor: pointer;
    height: 50px;
    width: 45px;
    border: 0;
    padding: 0;
    padding-left: 15px;
    border-radius: 0 2px 2px 0;
    background-color: #fff;
    background-image: url(../images/exclamation.svg);
    background-repeat: no-repeat;
    background-position: 50%;
}

.oam-FieldInputCheckboxTerms {

}

.oam-FieldInputCheckboxTerms_Container {
    display: inline-flex;
    position: relative;
}

.oam-FieldInputCheckboxTerms_Checkbox {
    width: 46px;
    height: 46px;
    margin-left: -13px;
    display: flex;
    flex-shrink: 0;
    align-items: center;
    justify-content: center;
    cursor: pointer;
}

.oam-FieldInputCheckboxTerms_Checkbox::after{
    background-position: 50%;
    background-repeat: no-repeat;
    border: 0;
    content: "";
    display: block;
    background: #fff;
    width: 20px;
    border-radius: 2px;
    flex-shrink: 0;
    height: 20px;
    border: 1px solid #b5b5b5;
    /* background-image: url(../images/check.svg);
    background-color: #126e51;
    background-size: 8px;
    background-position: 50%;
    background-repeat: no-repeat; */
    background-image: var(--custom-background-image);
    background-color: var(--custom-background-color);
    background-size: var(--custom-background-size);
    background-position: var(--custom-background-position);
    background-repeat: var(--custom-background-repeat);
}

.signinerrorprompt{
    margin-top: 30px;
    color: crimson;
}

.oam-OAFieldSubmitButton {
    margin-bottom: 30px;
    height: 50px;
    width: 100%;
    padding: 0 15px;
    border: none;
    border-radius: 2px;
    cursor: pointer;
    font-size: 15px;
    line-height: 18px;
    align-items: center;
    justify-content: center;
    display: flex;
    font-weight: 700;
    text-align: center;
    color: #fff;
    background-color: #FF6B0F;
}

.oam-FieldInputCheckboxTerms_Label {
    padding-top: 13px;
    font-size: 11px;
    color: #282828;
    line-height: 17px;
    padding-bottom: 13px;
}

.oam-FieldInputCheckboxTerms_Link{
    color: #007a56;
    font-weight: 700;
    text-decoration: none;
    cursor: pointer;
    line-height: 17px;
}

.oam-FieldInputCheckboxTerms_Link:hover{
    text-decoration: underline;
}
</style>