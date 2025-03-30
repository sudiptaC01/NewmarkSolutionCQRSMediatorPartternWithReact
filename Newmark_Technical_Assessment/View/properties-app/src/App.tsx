import React,{useEffect,useState} from 'react';

interface propertiesItem{
  features:string[],
  highlights:string[],
  propertyId: string,
  propertyName: string,
  transportation: TransportMode[],
  spaces: Space[]
}

type TransportMode={
  type: string,
  line: string,
  distance: string
}

type Space={
  spaceId: string,
  spaceName: string,
  rentRoll: RentDetails[]
}

type RentDetails={
  month: string,
  rent: string
}

const CollapsibleSection: React.FC<{
  title: string;
  children: React.ReactNode;
}> = ({ title, children }) => {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <div>
      <h3 onClick={() => setIsOpen(!isOpen)} style={{ cursor: 'pointer' }}>
        {title} {isOpen ? '- (Click to Collapse)' : '+ (Click to Expand)'}
      </h3>
      {isOpen && <div>{children}</div>}
    </div>
  );
};

function App() {
  const [properties, setProperties]= useState<propertiesItem[]>([]);

  // useEffect(()=>{
  //    fetch("https://localhost:7077/api/Properties")
  //   .then((response)=>response)
  //   .then((e)=>e.json())
  //   .then((e)=>setProperties(e as propertiesItem[]))
  // },[]);

const fetchData= async()=>{
  try
  {
    const response= await fetch("https://localhost:7077/api/Properties");
    if(!response.ok)
    {
      throw new Error("API call Failed");
    }
    const result= await response.json();
    setProperties(result as propertiesItem[]);
  }
  catch(error){
    console.log(error);
  }
}

fetchData();

  return (
    
    <div className="App">
      {properties.map((property)=>(
        <div key={property.propertyId}>

        <CollapsibleSection title="Property : ">
          <ul key={property.propertyId + property.propertyName}> 

          <li key={property.propertyId}>{property.propertyId}</li>
          <li key={property.propertyName}>{property.propertyName}</li>

          <CollapsibleSection title="Features">         
          <ul>
          {property.features.map((feature, index) => (
            <li key={index}>{feature}</li>
          ))}
          </ul>
          </CollapsibleSection>
        
        <CollapsibleSection title="Highlights">
        <ul>
        {property.highlights.map((highlight, index) => (
          <li key={index}>{highlight}</li>
        ))}
        </ul>
        </CollapsibleSection>

        <CollapsibleSection title="Spaces">
        <ul>
        {property.spaces.map((space, index) => (
          <li key={index}>
            <p>{space.spaceName}</p>
            
            <CollapsibleSection title="Rent">
            <ul>
              {space.rentRoll.map((rent, rentIndex) => (
                <li key={rentIndex}>
                  {rent.month}: {rent.rent}
                </li>
              ))}
            </ul>
            </CollapsibleSection>
          </li>
        ))}
      </ul>
      </CollapsibleSection>

      <CollapsibleSection title="Transportation">
        <ul>
        {property.transportation.map((transport, index) => (
          <li key={index}>
            {transport.type} - {transport.line} ({transport.distance})
          </li>
        ))}
      </ul> 
      </CollapsibleSection>   
      </ul>
      </CollapsibleSection>
      </div>        
      ))}      
    </div>
  );
}

export default App;
