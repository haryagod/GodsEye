import {  Component } from 'react';
class BucketList extends Component {
  
  componentDidMount(){
 this.props.onAwsFileList();
 }
  state = {  } 
  render()  {
    const {onVedioSelect,listFiles,onDelete} = this.props; 
    return (
      <div className='card'>
        <div className='card-header'>SampleCompany Files</div>
        <ul className='list-group'>
          {listFiles &&
            listFiles.map((name, index) => (
              <li className='list-group-item'  key={index}>
                <div onClick={()=>onVedioSelect(name.Key)}>{name.Key.split("/").pop()}</div > <button onClick={()=>onDelete(name.Key)}>delete</button>
              </li>
            ))}
        </ul>
       
      </div>);
  }
}
export default BucketList;