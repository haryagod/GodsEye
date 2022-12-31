import axios from 'axios';
axios.interceptors.response.use(null,error=>{

    const expectedError = error.response &&
    error.response >=400 && error.response <500;
    if(!expectedError)
    {
    console.log(error)
    }
    return Promise.reject();
})
export default{
   get: axios.get,
   post: axios.post,
   put: axios.put,
   delete: axios.delete,

}
