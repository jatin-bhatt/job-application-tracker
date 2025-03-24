import { KeyValuePair } from "../types/KeyValuePair";

type Args = {
    id: string | undefined;
    name: string | undefined;
    data: KeyValuePair[];
    value: string | number;
    className: string;
    onChange: (e: React.ChangeEvent<HTMLSelectElement>) => void;
};


const DropDownList = ({ id, name, data, value, className, onChange }: Args) => {
    return (
        <select id={id} name={name} className={className} defaultValue={value} value={value} onChange={ e => onChange(e)}>
            {data && data.map((item: KeyValuePair) => (
                <option key={item.key} value={item.key}>{ item.value }</option>
            ))}
        </select>
    )
}

export default DropDownList;