import { useState } from "react";

import InfoTab from "./InfoTab";
import PasswordTab from "./PasswordTab";

const infoTab = "infoTab";
const pwTab = "pwTab";

export default function AccountPage() {
	const [tab, setTab] = useState(infoTab);
	const handleTabClick = (e) => {
		e.preventDefault();
		setTab(e.target.name);
	};
	return (
		<div>
			<ul className="nav nav-tabs">
				<li className="nav-item">
					<a
						name={infoTab}
						className={`nav-link position-relative ${tab === infoTab ? "active" : ""}`}
						onClick={handleTabClick}
						aria-current="page"
						href="#">
						Thông tin tài khoản
					</a>
				</li>
				<li className="nav-item">
					<a
						name={pwTab}
						className={`nav-link position-relative ${tab === pwTab ? "active" : ""}`}
						onClick={handleTabClick}
						href="#">
						Thay đổi mật khẩu
					</a>
				</li>
			</ul>
			<div>
				{tab === infoTab && <InfoTab />}
				{tab === pwTab && <PasswordTab />}
			</div>
		</div>
	);
}
