import React, { Component } from "react";
import S3upload from "./common/s3upload";
import S3StreamVedio from "./common/s3StreamVedio";
import BucketList from "./common/s3VedioList";
import AWS from 'aws-sdk';
import Navbar from "./common/Appbar";

export default class App extends Component {
 state={
    awsConfig : {
      uploadProgressOfAll:[],  
    bucketName: 'vedios2',
    awsFileList:[],
    selectedVedio:'',
    dirName: 'NeverValidDirectory', /* optional */
     awsCred:{ 
        region: 'ap-south-1',
       accessKeyId: 'AKIA6MZM6VZYYYUTPC6K',
       secretAccessKey: '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'
     }
   }

 } 

 componentDidMount(){
  this.configureAWS();
 }
 componentDidUpdate(){
  console.log(this.state.awsConfig.uploadProgressOfAll)
 }

 //aws methods

 configureAWS(){
  const awsCred = {...this.state.awsConfig.awsCred}
  AWS.config.update(
   {...awsCred}
    );
 }
 handleResetDownloadList=()=>{
  
  const {awsConfig} = {...this.state}
  awsConfig.uploadProgressOfAll=[];
  this.setState({awsConfig});
  
 }

 handleAwsDirChange =async (dirName) =>
 {
  let awsConfig = {...this.state.awsConfig}
  awsConfig.dirName =dirName;
  await this.setState({awsConfig});
   await this.getawsFileList();
 }
 handleDelete = key =>{
  const {bucketName} = this.state.awsConfig
  const s3 = new AWS.S3();
  const params = {
    Bucket: bucketName,
    Key:key
  };      
s3.deleteObject(params, (err, data) => {
  this.getawsFileList();
 });
}
handleUpload = async e =>
{
  const {bucketName,dirName} = this.state.awsConfig
  const params = {
    Bucket: bucketName,
    Key:dirName+'/'+e.target.value.split("\\").pop(),
    Body:e.target.files[0]
  };

  var upload = new AWS.S3.ManagedUpload({
    params: params
  });
    await upload.on('httpUploadProgress',((progress)=> {
      progress.loaded =progress.loaded
      progress.total =progress.total
     let uploadProgressOfAll =[...this.state.awsConfig.uploadProgressOfAll]
     const isOld = uploadProgressOfAll.filter(obj=>obj.key == progress.key  )[0]
     if(!isOld)
     uploadProgressOfAll.push({...progress});
     else
    {
      const index = uploadProgressOfAll.findIndex((el) => el.key === progress.key)
      uploadProgressOfAll[index] = progress;
    }
    const {awsConfig} = {...this.state}
    awsConfig.uploadProgressOfAll =uploadProgressOfAll;
     this.setState({awsConfig});
    
  }));
 await upload.promise().then((( data,err)=> {
  this.uploadComplete(data);
  }));
}
uploadComplete(data)
{
  this.getawsFileList();
}
handleVedioSelect = (selectedVedio) =>{
  const {bucketName} = this.state.awsConfig
  const {region}= this.state.awsConfig.awsCred
  selectedVedio = "https://"+bucketName+".s3."+region+".amazonaws.com/" + selectedVedio
  let {awsConfig} = {...this.state}
  awsConfig.selectedVedio =selectedVedio;
this.setState({awsConfig})

}
getawsFileList= async() =>{
  this.configureAWS();
const {bucketName,dirName} = this.state.awsConfig
    const s3 = new AWS.S3();
    const params = {
      Bucket: bucketName,
      Delimiter: '',
      Prefix: dirName,
    };     
 await s3.listObjectsV2(params, (err, data) => {
  if (err) {
    console.log(err, err.stack);
    
  } else {
    const awsConfig ={...this.state.awsConfig}
    awsConfig.awsFileList=data.Contents;
      //delete data.Contents[0]
      this.setState ({awsConfig});
  }
});
}
//end Aws methods

render(){
  return (
       <React.Fragment>
          <Navbar
        onDelete ={this.handleDelete}
        onResetDownloadList ={this.handleResetDownloadList}
         onAwsFileList = {this.getawsFileList}
        onVedioSelect = {this.handleVedioSelect}
        onUpload = {this.handleUpload}
        awsConfig ={this.state.awsConfig}
        changeAwsDir={this.handleAwsDirChange}
        AppName="Student" onSidebarOpen={this.handleSideBar}/>
      </React.Fragment>
  )}
}; 