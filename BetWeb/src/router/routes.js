const routes = [
    /* 前台 */
    {
        path:"/envTest",
        component:()=>import("../pages/ForeGround/envTest.vue"),
    },
    {
        path:"/sports/basketball",
        component:()=>import("../pages/ForeGround/sports/basketball.vue"),
    },
    {
        path:"/signout",
        component:()=>import("../pages/signout.vue"),
    },
    {
        path:"/signin",
        component:()=>import("../pages/signin.vue"),
    },
    {
        path:"/signup",
        component:()=>import("../pages/signup.vue"),
    },
    /* 後台 */
    /* 資料來源 */
    {
        path:"/sportData",
        component:()=>import("../pages/SportData/sportData.vue"),
    },
]

export default routes;