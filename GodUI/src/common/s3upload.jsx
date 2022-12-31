
 const S3upload = ({onUpload}) => {  
  return (
        <form>
          <div className="form-group">
          
            <div className="mb-3">
    <label htmlFor="formFile" className="form-label">Upload File</label>
    <input  onChange={onUpload} className="form-control" type="file" id="formFile"/>
  </div>
          </div>
        </form>
      
    );
  };

  export default S3upload;