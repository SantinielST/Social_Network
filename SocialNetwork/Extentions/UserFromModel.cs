﻿using SocialNetwork.BLL.Models;
using SocialNetwork.ViewModels;

namespace SocialNetwork.Extentions;

public static class UserFromModel
{
    public static User Convert(this User user, UserEditViewModel usereditvm)
    {
        user.Image = usereditvm.Image;
        user.LastName = usereditvm.LastName;
        user.FirstName = usereditvm.FirstName;
        user.MiddleName = usereditvm.MiddleName;
        user.Email = usereditvm.Email;
        user.BirthDate = usereditvm.BirthDate;
        user.UserName = usereditvm.FirstName;
        user.Status = usereditvm.Status;
        user.About = usereditvm.About;

        return user;
    }
}