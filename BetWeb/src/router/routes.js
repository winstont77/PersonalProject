const routes = [
    {
        path:"/index",
        component:()=>import("../pages/index.vue")
    },
    {
        path:"/test",
        component:()=>import("../pages/test.vue")
    },
]

export default routes;